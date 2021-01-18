using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Controllers
{
    public class ShipController : Controller
    {
        // GET: Ship
        public ActionResult Index()
        {
            if (Session["ship_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}