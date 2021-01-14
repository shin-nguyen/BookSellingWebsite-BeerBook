using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Book.Models;
using Book.Models.DTO;

namespace Book.Controllers
{
    public class AdminController : Controller
    {
        //db

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

        public ActionResult BookManagement()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult OrderManagement()
        {
            if(Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}