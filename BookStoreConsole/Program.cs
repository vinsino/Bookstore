using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models;

namespace BookStoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           
            using (BookService service = new BookService(new BookDBRepository()))
            {
                var books = service.GetAll();

                foreach (Book book in books)
                {
                    Console.WriteLine($"{book.BookId}   {book.AuthorName}   {book.BookPrice} ");
                }
            }

            Console.ReadKey();
        }
    }
}
