using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Controllers
{
    public class HomeScreenController : Controller
    {
        // GET: HomeScreen
        public ActionResult Index()
        {
            return View();
        }
    }
}