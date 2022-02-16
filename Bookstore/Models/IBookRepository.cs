using System.Collections.Generic;

namespace Bookstore.Models
{
    public interface IBookRepository
    {
        bool Delete(int BookId);
        void Dispose();
        Book Get(int BookId);
        List<Book> GetAll();
        bool Insert(Book book);
        bool Update(Book book);
    }
}