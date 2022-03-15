using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class BookService : IDisposable
    {
        private DBUnitOfWork _dbContext;

        public BookService()
        {
            _dbContext = new DBUnitOfWork();
        }             
        public List<Book> GetAll()
        {
            var books = _dbContext.Book.GetAll();
            return books.Where(p => p.BookPrice >= 50).ToList();
        }
        public Book Get(int id)
        {
            return _dbContext.Book.Get(id);
        }
        public bool Insert(Book book)
        {
            if (book.BookPrice < 50)
                book.BookPrice = 50;
            var ret = _dbContext.Book.Insert(book);
            _dbContext.Commit();
            return ret;
        }
        public bool Update(Book book)
        {
            if (book.BookPrice < 50)
                book.BookPrice = 50;
            var ret = _dbContext.Book.Update(book);
            _dbContext.Commit();
            return ret;
        }
        public bool Delete(int id)
        {
            var ret = _dbContext.Book.Delete(id);
            _dbContext.Commit();
            return ret;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}