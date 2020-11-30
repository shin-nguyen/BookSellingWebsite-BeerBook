using BookSellingWebsite_BeerBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSellingWebsite_BeerBook.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        public ActionResult ListAuthorization()
        {
            var Authorizations = MainController.getAllAuthorization();
            return View(Authorizations);
        }
        public ActionResult EditAuthorization(int id)
        {
            var Authorizations = MainController.getAllAuthorization();
            Authorization_ Authorization = new Authorization_();
            foreach (Authorization_ a in Authorizations)
            {
                if (a.AuID_ == id)
                {
                    Authorization = a;
                    break;
                }
            }
            if (Authorization == null)
            {
                return HttpNotFound();
            }
            return View(Authorization);
        }
        [HttpPost]
        public ActionResult EditAuthorization(Authorization_ Authorization)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.Authorization_.AddOrUpdate(Authorization);
                _context.SaveChanges();
                return RedirectToAction("ListAuthorization");
            }
        }
        public ActionResult AddAuthorization()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAuthorization(Authorization_ Authorization)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.Authorization_.AddOrUpdate(Authorization);
                _context.SaveChanges();
                return RedirectToAction("ListAuthorization");
            }
        }

        public ActionResult DetailAuthorization(int id)
        {
            Authorization_ Authorization = MainController.getAAuthorization(id);
            ViewBag.AuthorizationID = Authorization.AuID_;
            if (Authorization == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Authorization);
        }

        public ActionResult DeleteAuthorization(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var Authorization = (from u in _context.Authorization_
                              where u.AuID_ == id
                              select u).SingleOrDefault();
                _context.Authorization_.Remove(Authorization);
                _context.SaveChanges();
                return RedirectToAction("ListAuthorization");
            }

        }
    }
}