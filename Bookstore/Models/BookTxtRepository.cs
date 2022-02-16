using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class BookTxtRepository : IDisposable, IBookRepository
    {
        private string _appData = "";
        public BookTxtRepository()
        {
            _appData = HttpContext.Current.Server.MapPath("~/App_Data");
        }
        public List<Book> GetAll()
        {
            var bookList = System.IO.Directory.GetFiles(_appData, "*.txt").OrderBy(p =>
            System.IO.Path.GetFileName(p)).ToList();
            List<Book> books = new List<Book>();
            foreach (var item in bookList)
            {
                string[] bookText = System.IO.File.ReadAllLines(item);
                Book book = new Book();
                book.BookId = Convert.ToInt32(bookText[0]);
                book.AuthorName = bookText[1];
                book.BookPrice = Convert.ToInt32(bookText[2]);
                books.Add(book);
            }
            return books;
        }
        public Book Get(int BookId)
        {
            string fn = Path.Combine(_appData, BookId.ToString() + ".txt");
            string[] bookText = File.ReadAllLines(fn);
            Book book = new Book();
            book.BookId = Convert.ToInt32(bookText[0]);
            book.AuthorName = bookText[1];
            book.BookPrice = Convert.ToInt32(bookText[2]);
            return book;
        }
        public bool Insert(Book book)
        {
            return Update(book);
        }
        public bool Update(Book book)
        {
            string fn = Path.Combine(_appData, book.BookId.ToString() + ".txt");
            string[] bookText = new string[3];
            bookText[0] = book.BookId.ToString();
            bookText[1] = book.AuthorName;
            bookText[2] = book.BookPrice.ToString();
            bool ret = false;
            try
            {
                System.IO.File.WriteAllLines(fn, bookText);
                
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }
        public bool Delete(int BookId)
        {
            string fn = Path.Combine(_appData, BookId.ToString() + ".txt");
            bool ret = false;
            if (!System.IO.File.Exists(fn)) return ret;
            try
            {
                System.IO.File.Delete(fn);
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }
        public void Dispose()
        {
            return;
        }

    }


}