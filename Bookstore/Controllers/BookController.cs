using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            List<Book> books = new List<Book>();

            string[] files = Directory.GetFiles(Server.MapPath("~/App_Data"), "*.txt", SearchOption.TopDirectoryOnly);
            foreach (var item in files)
            {
                string[] lines = System.IO.File.ReadAllLines(item);
                books.Add(new Book { BookId = Convert.ToInt32(lines[0]), AuthorName = lines[1], BookPrice = Convert.ToInt32(lines[2]) });
            }

            books = books.OrderBy(book => book.BookId).ToList();

            return View(books);
        }



        public ActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book model)
        {

            if (!ModelState.IsValid)
                return View();
           
            string path = Path.Combine(Server.MapPath("~/App_Data"), $"{model.BookId}.txt");

            if (!System.IO.File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine(model.BookId);
                    sw.WriteLine(model.AuthorName);
                    sw.WriteLine(model.BookPrice);
                }

                return RedirectToAction("Index");
            }
            
            return View();
        }


        public ActionResult Details(int id)
        {
            string path = Path.Combine(Server.MapPath("~/App_Data"), $"{id}.txt");

            string[] lines = System.IO.File.ReadAllLines(path);
            Book book = new Book { BookId = Convert.ToInt32(lines[0]), AuthorName = lines[1], BookPrice = Convert.ToInt32(lines[2]) };


            return View(book);
        }

        public ActionResult Edit(int id)
        {
            string path = Path.Combine(Server.MapPath("~/App_Data"), $"{id}.txt");

            string[] lines = System.IO.File.ReadAllLines(path);
            Book book = new Book { BookId = Convert.ToInt32(lines[0]), AuthorName = lines[1], BookPrice = Convert.ToInt32(lines[2]) };


            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book model)
        {
            if (!ModelState.IsValid) 
                return View();
            
            string path = Path.Combine(Server.MapPath("~/App_Data"), $"{id}.txt");

            if (id != model.BookId)
            {
                string newpath = Path.Combine(Server.MapPath("~/App_Data"), $"{model.BookId}.txt");

                if (System.IO.File.Exists(newpath))                
                    return View();
                
                System.IO.File.Move(path, newpath);
                path = newpath;
            }

            // Open a file to overwrite to
            using (StreamWriter sw = System.IO.File.CreateText(path))
            {
                sw.WriteLine(model.BookId);
                sw.WriteLine(model.AuthorName);
                sw.WriteLine(model.BookPrice);
            }

            return RedirectToAction("Index");

        }

        
        public ActionResult Delete(int id)
        {
            string path = Path.Combine(Server.MapPath("~/App_Data"), $"{id}.txt");

            string[] lines = System.IO.File.ReadAllLines(path);
            Book book = new Book { BookId = Convert.ToInt32(lines[0]), AuthorName = lines[1], BookPrice = Convert.ToInt32(lines[2]) };

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string confirmButton)
        {
            string path = Path.Combine(Server.MapPath("~/App_Data"), $"{id}.txt");
            System.IO.File.Delete(path);
            return RedirectToAction("Index");
        }
    }
}