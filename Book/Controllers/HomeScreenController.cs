using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book.Models;

namespace Book.Controllers
{
    public class HomeScreenController : Controller
    {
        dbbookEntities db = new dbbookEntities();
        // GET: HomeScreen
        public ActionResult Index()
        {
            List<tbl_book> books = db.tbl_book.OrderBy(x => x.book_id).ToList();
            var top5books = (from tbl_book in books
                             orderby tbl_book.book_id descending
                             select tbl_book).Take(5);
            return View(top5books);
        }

        //YourAccount
        public ActionResult YourAccount()
        {
            if(Session["user_id"]== null)
            {
                return RedirectToAction("Login", "Account");
            }

            int accid = Convert.ToInt32(Session["user_id"]);
            tbl_customer c = db.tbl_customer.Where(x => x.cus_acc_fk == accid).SingleOrDefault();

            return View(c);
        }

    }
}