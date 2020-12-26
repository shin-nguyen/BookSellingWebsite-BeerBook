using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Controllers
{
    public class HomeScreenController : Controller
    {
        private dbbookEntities _context;
        // GET: HomeScreen
        public HomeScreenController()
        {
            _context = new dbbookEntities();
        }
        //GET: HomeScreen/
        public ActionResult Index()
        {
            List<tbl_book> allBooks = _context.Database.SqlQuery<tbl_book>("Sp_Book_List").ToList();
            return View(allBooks);
        }

        public ActionResult Store()
        {
            ViewModel viewModel = new ViewModel();

            viewModel.allBooks = _context.Database.SqlQuery<tbl_book>("Sp_Book_List").ToList();
            viewModel.allCatigories = _context.Database.SqlQuery<tbl_category>("Sp_Categories_List").ToList();

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            tbl_book BookDetail = _context.Database.SqlQuery<tbl_book>("Sp_Book_Details @id = {0}", id).Single();
            ViewBag.ProId = id;
            return View(BookDetail);
        }
        public ActionResult YourAccount()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int accid = Convert.ToInt32(Session["user_id"]);
            tbl_customer c = _context.tbl_customer.Where(x => x.cus_acc_fk == accid).SingleOrDefault();

            return View(c);
        }
    }
}