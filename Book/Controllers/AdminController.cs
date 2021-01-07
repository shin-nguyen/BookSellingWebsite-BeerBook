using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book.Models;

namespace Book.Controllers
{
    public class AdminController : Controller
    {
        //db
        //dbbookEntities db = new dbbookEntities();

        private dbbookEntities _context;

        public AdminController()
        {
            this._context = new dbbookEntities();
        }

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult CategoryManagement()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult PublisherManagement()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult AuthorManagement()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}