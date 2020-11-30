using BookSellingWebsite_BeerBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSellingWebsite_BeerBook.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult ListAuthor()
        {
            var authors = MainController.getAllAuthor();
            return View(authors);
        }
        public ActionResult EditAuthor(int id)
        {
            var authors = MainController.getAllAuthor();
            Author_ author = new Author_();
            foreach (Author_ a in authors)
            {
                if (a.AuthorID_ == id)
                {
                    author = a;
                    break;
                }
            }
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }
        [HttpPost]
        public ActionResult EditAuthor(Author_ author)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.Author_.AddOrUpdate(author);
                _context.SaveChanges();
                return RedirectToAction("ListAuthor");
            }
        }
        public ActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAuthor(Author_ author)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.Author_.AddOrUpdate(author);
                _context.SaveChanges();
                return RedirectToAction("ListAuthor");
            }
        }

        public ActionResult DetailAuthor(int id)
        {
            Author_ author = MainController.getAAuthor(id);
            ViewBag.AuthorID = author.AuthorID_;
            if (author == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(author);
        }

        public ActionResult DeleteAuthor(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var author = (from u in _context.Author_
                            where u.AuthorID_ == id
                            select u).SingleOrDefault();
                _context.Author_.Remove(author);
                _context.SaveChanges();
                return RedirectToAction("ListAuthor");
            }

        }
    }
}