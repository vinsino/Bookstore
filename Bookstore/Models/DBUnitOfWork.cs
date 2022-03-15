using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class DBUnitOfWork : IDisposable
    {

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private BookDBRepository _bookRepository;
        private bool _disposed;
        
        public IDbTransaction Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _connection.Open();
                    _transaction = _connection.BeginTransaction();
                }
                return _transaction;
            }
        }
        public BookDBRepository Book
        {
            get { return _bookRepository ?? (_bookRepository = new BookDBRepository(this.Transaction)); }  // ?? null-coalescing operator  ??= null-coalescing assignment operator
        }
        

        public DBUnitOfWork() : this(ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString) // start connection in uow instead of individual repository
        {
        }
        
        public DBUnitOfWork(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);  
        }

        //---------------------------


        private void ResetRepositories()
        {
            _bookRepository = null;
        }

        public virtual void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                this.ResetRepositories();
            }
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~DBUnitOfWork()
        {
            dispose(false);
        }
    }
}