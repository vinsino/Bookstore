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

        private IBookRepository _repository;
        public BookController()
        {
            _repository = new BookDBRepository();
            //_repository = new BookTxtRepository();

        }


        // GET: Book
        public ActionResult Index()
        {
            var ret = _repository.GetAll();
            return View(ret);
        }



        public ActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
                return View();

            bool ret = _repository.Insert(book);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var ret = _repository.Get(id);
            return View(ret);
        }

        public ActionResult Edit(int id)
        {
            var ret = _repository.Get(id);
            return View(ret);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book book)
        {
            if (!ModelState.IsValid) 
                return View();
            
            bool ret = _repository.Update(book);
            return RedirectToAction("Index");

        }

        
        public ActionResult Delete(int id)
        {
            var ret = _repository.Get(id);
            return View(ret);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string confirmButton)
        {
            var ret = _repository.Delete(id);

            if (!ret) return View();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}