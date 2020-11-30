using BookSellingWebsite_BeerBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSellingWebsite_BeerBook.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult ListCustomer()
        {
            var custommers = MainController.getAllCustomer();
            return View(custommers);
        }
        public ActionResult EditCustomer(int id)
        {
            var Customers = MainController.getAllCustomer();
            Customer_ Customer = new Customer_();
            foreach (Customer_ x in Customers)
            {
                if (x.CustomerID_ == id)
                {
                    Customer = x;
                    break;
                }
            }
            if (Customer == null)
            {
                return HttpNotFound();
            }
            return View(Customer);
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer_ Customer)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.Customer_.AddOrUpdate(Customer);
                _context.SaveChanges();
                return RedirectToAction("ListCustomer");
            }
        }
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(Customer_ Customer)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.Customer_.AddOrUpdate(Customer);
                _context.SaveChanges();
                return RedirectToAction("ListCustomer");
            }
        }

        public ActionResult DetailCustomer(int id)
        {
            Customer_ Customer = MainController.getACustomer(id);
            ViewBag.CustomerID = Customer.CustomerID_;
            if (Customer == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Customer);
        }

        public ActionResult DeleteCustomer(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var Customer = (from u in _context.Customer_
                              where u.CustomerID_ == id
                              select u).SingleOrDefault();
                _context.Customer_.Remove(Customer);
                _context.SaveChanges();
                return RedirectToAction("ListCustomer");
            }

        }
    }
}