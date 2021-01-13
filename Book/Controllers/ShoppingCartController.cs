using Book.Models;
using Book.Models.Consts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Book.Controllers
{
    public class ShoppingCartController : Controller
    {
        dbbookEntities _db = new dbbookEntities();
        // GET: ShoppingCart
        // add item vao gio hang
        public ActionResult AddtoCart(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index", "HomeScreen");
            }

            int userid = Convert.ToInt32(Session["user_id"]);

            var product = this._db.tbl_book.Find(id);

            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            if (product.book_quantity == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            tbl_cart c = _db.tbl_cart.Where(x => x.cart_fk_cusid == userid && x.cart_fk_bookid == id).SingleOrDefault();

            if (c == null)
            {
                tbl_cart cart = new tbl_cart();
                cart.cart_fk_bookid = id;
                cart.cart_fk_cusid = userid;
                cart.cart_book_amount = 1;
                _db.tbl_cart.Add(cart);
                _db.SaveChanges();
            }
            else
            {
                c.cart_book_amount += 1;
                _db.tbl_cart.AddOrUpdate(c);
                _db.SaveChanges();
            }

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        // trang gio hang
        public ActionResult ShowToCart()
        {
            int userid = Convert.ToInt32(Session["user_id"]);
            List<tbl_cart> cart = _db.tbl_cart.Where(x => x.cart_fk_cusid == userid).ToList();
            return View(cart);
        }
        //
        public ActionResult Update_Quantity_Cart(int ID_Product, int quantity)
        {
            int userid = Convert.ToInt32(Session["user_id"]);
            tbl_cart cart = _db.tbl_cart.Where(x => x.cart_fk_cusid == userid && x.cart_fk_bookid == ID_Product).SingleOrDefault();

            var product = this._db.tbl_book.Find(ID_Product);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            if (product.book_quantity < quantity - cart.cart_book_amount)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            cart.cart_book_amount = quantity;
            _db.tbl_cart.AddOrUpdate(cart);
            _db.SaveChanges();
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        //
        public ActionResult RemoveCart(int id)
        {
            int userid = Convert.ToInt32(Session["user_id"]);
            tbl_cart cart = _db.tbl_cart.Where(x => x.cart_fk_cusid == userid && x.cart_fk_bookid == id).SingleOrDefault();
            _db.tbl_cart.Remove(cart);
            _db.SaveChanges();
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveAllCart()
        {
            int userid = Convert.ToInt32(Session["user_id"]);
            List<tbl_cart> cart = _db.tbl_cart.Where(x => x.cart_fk_cusid == userid).ToList();
            foreach (var i in cart)
            {
                _db.tbl_cart.Remove(i);

            }
            _db.SaveChanges();

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult CreateAnOrder()
        {
            int customerID = Convert.ToInt32(Session["user_id"]);

            var carts = this._db.tbl_cart.Where(c => c.cart_fk_cusid == customerID).ToList();

            if (carts.Count() == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            foreach (var cart in carts)
            {
                if (cart.cart_book_amount > cart.tbl_book.book_quantity)
                    throw new Exception("Invalid amount of products inserting to orders");
            }

            int noOfRowEffected = this._db.Database.ExecuteSqlCommand("Insert into tbl_order(order_fk_cusid) Values(" + customerID + ")");

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        //
        public PartialViewResult BagCart()
        {
            if(Session["user_id"] == null)
            {
                int userid = Convert.ToInt32(Session["user_id"]);
                var amount = (from i in _db.tbl_cart
                              where i.cart_fk_cusid == userid
                              select (int?)i.cart_book_amount).Sum() ?? 0;
                ViewBag.infoCart = amount;
            }
            return PartialView("BagCart");
        }
    }
}