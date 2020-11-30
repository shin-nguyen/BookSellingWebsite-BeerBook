using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookSellingWebsite_BeerBook.Models;

namespace BookSellingWebsite_BeerBook.Controllers
{
    public class BookController : Controller
    {
        public ActionResult ListBook()
        {
            var books = MainController.getAllBooks();
            return View(books);
        }
        public ActionResult EditBook(int id)
        {
            var books = MainController.getAllBooks();
            Book_ book = new Book_();
            foreach(Book_ b in books)
            {
                if(b.BookID_== id)
                {
                    book = b;
                    break;
                }    
            }
            if(book == null)
            {
                return HttpNotFound();
            }    
            return View(book);
        }
        [HttpPost]
        public ActionResult EditBook(Book_ book)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.Book_.AddOrUpdate(book);
                _context.SaveChanges();
                return RedirectToAction("ListBook");
            }
        }

        public ActionResult AddBook()
        {
            var Author = MainController.getAllAuthor();
            var BookHouse = MainController.getAlLBookHouse();
            ViewBag.AuthorID_ = new SelectList(Author.ToList().OrderBy(n => n.Name_), "AuthorID_", "Name_");
            ViewBag.BhID_ = new SelectList(BookHouse.ToList().OrderBy(x => x.Name_), "BhID_", "Name_");
            return View();
        }
        [HttpPost]
        public ActionResult AddBook(Book_ book, HttpPostedFileBase fileupload)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.Book_.AddOrUpdate(book);
                _context.SaveChanges();
                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/BookImages"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileupload.SaveAs(path);
                }
                var Author = MainController.getAllAuthor();
                var BookHouse = MainController.getAlLBookHouse();
                ViewBag.AuthorID_ = new SelectList(Author.ToList().OrderBy(n => n.Name_), "AuthorID_", "Name_");
                ViewBag.BhID_ = new SelectList(BookHouse.ToList().OrderBy(x => x.Name_), "BhID_", "Name_");
                return RedirectToAction("ListBook");
            }
        }

        public ActionResult DetailBook(int id)
        {
            Book_ book = MainController.getABook(id);
            ViewBag.BookID = book.BookID_;
            if(book==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(book);
        }

        public ActionResult DeleteBook(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var book = (from u in _context.Book_
                              where u.BookID_ == id
                              select u).SingleOrDefault();
                _context.Book_.Remove(book);
                _context.SaveChanges();
                return RedirectToAction("ListBook");
            }

        }
    }
}