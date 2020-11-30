using BookSellingWebsite_BeerBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSellingWebsite_BeerBook.Controllers
{
    public class PayMethodController : Controller
    {
        // GET: PayMethod
        public ActionResult ListPayMethod()
        {
            var PayMethods = MainController.getAllPayMethod();
            return View(PayMethods);
        }
        public ActionResult EditPayMethod(int id)
        {
            var PayMethods = MainController.getAllPayMethod();
            PayMethod_ PayMethod = new PayMethod_();
            foreach (PayMethod_ a in PayMethods)
            {
                if (a.PayID_ == id)
                {
                    PayMethod = a;
                    break;
                }
            }
            if (PayMethod == null)
            {
                return HttpNotFound();
            }
            return View(PayMethod);
        }
        [HttpPost]
        public ActionResult EditPayMethod(PayMethod_ PayMethod)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.PayMethod_.AddOrUpdate(PayMethod);
                _context.SaveChanges();
                return RedirectToAction("ListPayMethod");
            }
        }
        public ActionResult AddPayMethod()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPayMethod(PayMethod_ PayMethod)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.PayMethod_.AddOrUpdate(PayMethod);
                _context.SaveChanges();
                return RedirectToAction("ListPayMethod");
            }
        }

        public ActionResult DetailPayMethod(int id)
        {
            PayMethod_ PayMethod = MainController.getAPayMethod(id);
            ViewBag.PayMethodID = PayMethod.PayID_;
            if (PayMethod == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(PayMethod);
        }

        public ActionResult DeletePayMethod(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var PayMethod = (from u in _context.PayMethod_
                              where u.PayID_ == id
                              select u).SingleOrDefault();
                _context.PayMethod_.Remove(PayMethod);
                _context.SaveChanges();
                return RedirectToAction("ListPayMethod");
            }

        }
    }
}