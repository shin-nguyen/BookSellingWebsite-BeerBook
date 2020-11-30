using BookSellingWebsite_BeerBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSellingWebsite_BeerBook.Controllers
{
    public class BookHouseController : Controller
    {
        // GET: BookHouse
        public ActionResult ListBookHouse()
        {
            var bookHouses = MainController.getAlLBookHouse();
            return View(bookHouses);
        }
        public ActionResult EditBookHouse(int id)
        {
            var BookHouses = MainController.getAlLBookHouse();
            BookHouse_ BookHouse = new BookHouse_();
            foreach (BookHouse_ x in BookHouses)
            {
                if (x.BhID_ == id)
                {
                    BookHouse = x;
                    break;
                }
            }
            if (BookHouse == null)
            {
                return HttpNotFound();
            }
            return View(BookHouse);
        }
        [HttpPost]
        public ActionResult EditBookHouse(BookHouse_ BookHouse)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.BookHouse_.AddOrUpdate(BookHouse);
                _context.SaveChanges();
                return RedirectToAction("ListBookHouse");
            }
        }
        public ActionResult AddBookHouse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBookHouse(BookHouse_ BookHouse)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.BookHouse_.AddOrUpdate(BookHouse);
                _context.SaveChanges();
                return RedirectToAction("ListBookHouse");
            }
        }

        public ActionResult DetailBookHouse(int id)
        {
            BookHouse_ bookhouse = MainController.getABookHouse(id);
            ViewBag.bookhouseID = bookhouse.BhID_;
            if (bookhouse == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(bookhouse);
        }

        public ActionResult DeleteBookHouse(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var bookhouse = (from u in _context.BookHouse_
                              where u.BhID_ == id
                              select u).SingleOrDefault();
                _context.BookHouse_.Remove(bookhouse);
                _context.SaveChanges();
                return RedirectToAction("ListBookHouse");
            }

        }
    }
}