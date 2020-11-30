using BookSellingWebsite_BeerBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSellingWebsite_BeerBook.Controllers
{
    public class PurchaseOrderController : Controller
    {
        // GET: PurchaseOrder
        public ActionResult ListPurchaseOrder()
        {
            var PurchaseOrders = MainController.getAllPurchaseOrders();
            return View(PurchaseOrders);
        }
        public ActionResult EditPurchaseOrder(int id)
        {
            var PurchaseOrders = MainController.getAllPurchaseOrders();
            PurchaseOrders_ PurchaseOrder = new PurchaseOrders_();
            foreach (PurchaseOrders_ b in PurchaseOrders)
            {
                if (b.PoID_ == id)
                {
                    PurchaseOrder = b;
                    break;
                }
            }
            if (PurchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(PurchaseOrder);
        }
        [HttpPost]
        public ActionResult EditPurchaseOrder(PurchaseOrders_ PurchaseOrder)
        {
            using (var _context = new BEERBOOKEntities())
            {
                _context.PurchaseOrders_.AddOrUpdate(PurchaseOrder);
                _context.SaveChanges();
                return RedirectToAction("ListPurchaseOrder");
            }
        }

        public ActionResult AddPurchaseOrder()
        {
            var Customer = MainController.getAllCustomer();
            var Authorization = MainController.getAllAuthorization();
            ViewBag.CustomerID_ = new SelectList(Customer.ToList().OrderBy(n => n.Name_), "CustomerID_", "Name_");
            ViewBag.AuID_ = new SelectList(Authorization.ToList().OrderBy(x => x.AuName_), "AuID_", "AuName_");
            return View();
        }
        [HttpPost]
        public ActionResult AddPurchaseOrder(PurchaseOrders_ PurchaseOrder, HttpPostedFileBase fileupload)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var PayMethod = MainController.getAllPayMethod();
                ViewBag.PayID_ = new SelectList(PayMethod.ToList().OrderBy(x => x.Name_), "PayID_", "Name_");
                return RedirectToAction("ListPurchaseOrder");
            }
        }

        public ActionResult DetailPurchaseOrder(int id)
        {
            PurchaseOrders_ PurchaseOrder = MainController.getAPurchaseOrders(id);
            ViewBag.PurchaseOrderID = PurchaseOrder.PoID_;
            if (PurchaseOrder == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(PurchaseOrder);
        }

        public ActionResult DeletePurchaseOrder(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var PurchaseOrder = (from u in _context.PurchaseOrders_
                                     where u.PoID_ == id
                                     select u).SingleOrDefault();
                _context.PurchaseOrders_.Remove(PurchaseOrder);
                _context.SaveChanges();
                return RedirectToAction("ListPurchaseOrder");
            }

        }
    }
}