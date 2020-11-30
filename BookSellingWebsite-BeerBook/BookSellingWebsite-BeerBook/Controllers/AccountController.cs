using BookSellingWebsite_BeerBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSellingWebsite_BeerBook.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult ListAccount()
        {
            var Accounts = MainController.getAllAccount();
            return View(Accounts);
        }
        public ActionResult EditAccount(int id)
        {
            var Accounts = MainController.getAllAccount();
            Account_ Account = new Account_();
            foreach (Account_ b in Accounts)
            {
                if (b.AccID_ == id)
                {
                    Account = b;
                    break;
                }
            }
            if (Account == null)
            {
                return HttpNotFound();
            }
            return View(Account);
        }
        [HttpPost]
        public ActionResult EditAccount(Account_ Account)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.Account_.AddOrUpdate(Account);
                _context.SaveChanges();
                return RedirectToAction("ListAccount");
            }
        }

        public ActionResult AddAccount()
        {
            var Customer = MainController.getAllCustomer();
            var Authorization = MainController.getAllAuthorization();
            ViewBag.CustomerID_ = new SelectList(Customer.ToList().OrderBy(n => n.Name_), "CustomerID_", "Name_");
            ViewBag.AuID_ = new SelectList(Authorization.ToList().OrderBy(x => x.AuName_), "AuID_", "AuName_");
            return View();
        }
        [HttpPost]
        public ActionResult AddAccount(Account_ Account, HttpPostedFileBase fileupload)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var Customer = MainController.getAllCustomer();
                var Authorization = MainController.getAllAuthorization();
                ViewBag.CustomerID_ = new SelectList(Customer.ToList().OrderBy(n => n.Name_), "CustomerID_", "Name_");
                ViewBag.AuID_ = new SelectList(Authorization.ToList().OrderBy(x => x.AuName_), "AuID_", "AuName_");
                return RedirectToAction("ListAccount");
            }
        }

        public ActionResult DetailAccount(int id)
        {
            Account_ Account = MainController.getAAccount(id);
            ViewBag.AccountID = Account.AccID_;
            if (Account == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Account);
        }

        public ActionResult DeleteAccount(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var Account = (from u in _context.Account_
                            where u.AccID_ == id
                            select u).SingleOrDefault();
                _context.Account_.Remove(Account);
                _context.SaveChanges();
                return RedirectToAction("ListAccount");
            }

        }
    }
}
