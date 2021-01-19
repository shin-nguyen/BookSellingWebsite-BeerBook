using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book.helper;
using Book.Models;

namespace Book.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        dbbookEntities _db = new dbbookEntities();

        public ActionResult OrderOfCus()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int cusId = Convert.ToInt32(Session["user_id"]);
            List<tbl_order> orders = this._db.tbl_order.Where(x => x.order_fk_cusid == cusId).OrderByDescending(x=>x.order_id).ToList();

            return View(orders);
        }

        public ActionResult OrderDetail(int? id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index", "HomeScreen");
            }

            List<tbl_oderdetail> ods = this._db.tbl_oderdetail.Where(x => x.od_fk_orderid == id).ToList();

            return View(ods);
        }

        public ActionResult CancelOrder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "HomeScreen");
            }
            tbl_order od = this._db.tbl_order.Where(x => x.order_id == id).SingleOrDefault();
            od.order_stt_fk = 4;
            this._db.SaveChanges();

            return RedirectToAction("OrderOfCus");
        }
        public ActionResult OrderOfShip()
        {
            if (Session["ship_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<tbl_order> orders = this._db.tbl_order.Where(x=>x.order_stt_fk==2||x.order_stt_fk==3).OrderByDescending(x => x.order_stt_fk).ToList();

            return View(orders);
        }
        public ActionResult OrderDetail1(int? id)
        {
            if (Session["ship_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index", "Ship");
            }

            List<tbl_oderdetail> ods = this._db.tbl_oderdetail.Where(x => x.od_fk_orderid == id).ToList();

            return View(ods);
        }
        public ActionResult OrderDone(int? id)
        {
            
            if (id == null)
            {
                return RedirectToAction("Index", "Ship");
            }
            tbl_order od = this._db.tbl_order.Where(x => x.order_id == id).SingleOrDefault();

            int customerID = (int)od.order_fk_cusid;

            od.order_stt_fk = 3;
            od.order_isPaid = true;

            try
            {
                GMailer gm = new GMailer();
                tbl_customer cus = _db.tbl_customer.Where(x => x.cus_id == customerID).SingleOrDefault();
                string body = "Dear " + cus.cus_name + " !" +
                                "\nThanks for your interest in us. Your order has been successfully delivered." +
                                "\nWish you have a great experience! ";
                gm.SendMail(cus.cus_mail, "Thanks for order!", body);
            }
            catch
            {

            }

            this._db.SaveChanges();

            return RedirectToAction("OrderOfShip");
        }
        public ActionResult OrderDetail2(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            List<tbl_oderdetail> ods = this._db.tbl_oderdetail.Where(x => x.od_fk_orderid == id).ToList();

            return View(ods);
        }
        public ActionResult ConformOrder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            tbl_order od = this._db.tbl_order.Where(x => x.order_id == id).SingleOrDefault();

            int customerID = (int)od.order_fk_cusid;

            od.order_stt_fk = 2;
            this._db.SaveChanges();

            try
            {
                GMailer gm = new GMailer();
                tbl_customer cus = _db.tbl_customer.Where(x => x.cus_id == customerID).SingleOrDefault();
                string body = "Dear " + cus.cus_name + " !" +
                                "\nThanks for your interest in us. Your order has been confirmed and delivered to a carrier. We will deliver the goods as soon as possible."+
                                "\nWish you have a great experience! ";
                gm.SendMail(cus.cus_mail, "Thanks for order!", body);
            }
            catch
            {

            }

            return RedirectToAction("OrderManagement","Admin");
        }
        public ActionResult DeleteOrder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            List<tbl_oderdetail> ods = this._db.tbl_oderdetail.Where(x => x.od_fk_orderid == id).ToList();
            foreach(var i in ods)
            {
                this._db.tbl_oderdetail.Remove(i);
            }
            this._db.SaveChanges();

            tbl_order or = this._db.tbl_order.Where(x => x.order_id == id).SingleOrDefault();
            this._db.tbl_order.Remove(or);
            this._db.SaveChanges();

            return RedirectToAction("OrderManagement", "Admin");
        }
    }
}