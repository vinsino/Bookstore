using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookstore.Filters;


namespace Bookstore.Controllers
{
    [MyAuthorize]
    public class Book1Controller : Controller
    {

        private BookService _service;
        public Book1Controller()
        {
            //_service = new BookService(new BookDBRepository());
            //_service = new BookService(new BookTxtRepository());
            _service = new BookService();
        }

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
               
        public ActionResult Create()
        {          
            return View();
        }

        public ActionResult Details(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult Delete(int id)
        {
            ViewBag.id = id;            
            return View();
        }


        // Web api---
        [HttpPost]
        public JsonResult GetAllBooks()
        {
            var ret = _service.GetAll();
            return Json(ret);
        }

        [HttpPost]
        public JsonResult GetBook(int id)
        {
            var ret = _service.Get(id);
            return Json(ret);
        }

        [HttpPost]
        public bool CreateBook(Book book)
        {           
            bool ret = _service.Insert(book);
            return ret;
        }


        [HttpPost]
        public bool UpdateBook(Book book)
        {
            var ret = _service.Update(book);
            return ret;
        }

        [HttpPost]
        public bool DeleteBook(int id)
        {
            var ret = _service.Delete(id);
            return ret;
        }


        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}