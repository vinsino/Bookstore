using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class BookService : IDisposable
    {
        private IBookRepository _repository;

        public BookService()
        {
            _repository = new BookDBRepository();
        }

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public List<Book> GetAll()
        {
            var books = _repository.GetAll();
            return books.Where(p => p.BookPrice >= 50).ToList();
        }
        public Book Get(int id)
        {
            return _repository.Get(id);
        }
        public bool Insert(Book book)
        {
            if (book.BookPrice < 50)
                book.BookPrice = 50;
            return _repository.Insert(book);
        }
        public bool Update(Book book)
        {
            if (book.BookPrice < 50)
                book.BookPrice = 50;
            return _repository.Update(book);
        }
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}