﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace Bookstore.Models
{
    public class BookDBRepository : IBookRepository, IDisposable
    {
        private string _bookConnection = "";
        private IDbConnection conn;
        
        public BookDBRepository()
        {
            _bookConnection = ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;
            conn = new SQLiteConnection(_bookConnection);
        }
        public List<Book> GetAll()
        {
            List<Book> ret;
            
            string sql = @"select * from books order by BookId";
            ret = conn.Query<Book>(sql).ToList();
            
            return ret;
        }
        public Book Get(int id)
        {
            Book ret;
            
            string sql = @"select * from books where BookId=@id";
            ret = conn.Query<Book>(sql, new { id }).SingleOrDefault();
            
            return ret;
        }
        public bool Insert(Book book)
        {
            int ret;
            
            string sql = @"Insert into books (BookId, AuthorName, BookPrice) values (@BookId, @AuthorName, @BookPrice)";
            ret = conn.Execute(sql, book);
            
            return ret > 0 ? true : false;
        }
        public bool Update(Book book)
        {
            int ret;
            
            string sql = @"Update books Set AuthorName=@AuthorName, BookPrice=@BookPrice where BookId=@BookId";
            ret = conn.Execute(sql, book);
            
            return ret > 0 ? true : false;
        }
        public bool Delete(int id)
        {
            int ret;
           
            string sql = @"delete books where BookId=@id";
            ret = conn.Execute(sql, new { id });
           
            return ret > 0 ? true : false;
        }
        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
            return;
        }
    }
}