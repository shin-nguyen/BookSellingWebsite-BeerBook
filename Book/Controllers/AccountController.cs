using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Controllers
{
    public class AccountController : Controller
    {
        //dbcontext, cyptop
        dbbookEntities db = new dbbookEntities();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //logout
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();

            return RedirectToAction("Index","HomeScreen");
        }

        //login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_account a)
        {
            try
            {
                string pwd = Cyptop.Encrypt(a.acc_password, true).ToString();

                tbl_account acc = db.tbl_account.Where(x => x.acc_username == a.acc_username && x.acc_password == pwd).SingleOrDefault();


                if (acc != null)
                {
                    if (acc.acc_role_fk == 1)
                    {
                        Session["ad_id"] = acc.acc_id;
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (acc.acc_role_fk == 2)
                    {
                        Session["ship_id"] = acc.acc_id;
                        return RedirectToAction("Index", "Ship");
                    }
                    else
                    {
                        Session["user_id"] = acc.acc_id;
                        return RedirectToAction("Index", "HomeScreen");
                    }
                }
                else
                {
                    ViewBag.msg = "Invalid username or password";
                }
            }
            catch { }
            return View();
        }

        //Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterClass r)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tbl_account a = db.tbl_account.Where(x => x.acc_username == r.Username).SingleOrDefault();
                    if (a == null)
                    {
                        tbl_account acc = new tbl_account();
                        int role = 3;

                        acc.acc_username = r.Username;
                        acc.acc_password = Cyptop.Encrypt(r.Password, true);
                        acc.acc_role_fk = role;
                        db.tbl_account.Add(acc);
                        db.SaveChanges();

                        int acc_id = db.tbl_account.Where(x => x.acc_username == r.Username).SingleOrDefault().acc_id;
                        tbl_customer cus = new tbl_customer();
                        cus.cus_name = r.Name;
                        cus.cus_phone = r.Phone;
                        cus.cus_address = r.Address;
                        cus.cus_acc_fk = acc_id;
                        db.tbl_customer.Add(cus);
                        db.SaveChanges();

                        ViewBag.msg1 = " You can back to login!";
                    }
                    else
                    {
                        ViewBag.msg2 = " Username already exists!";
                    }
                }
                else
                {
                    ViewBag.msg0 = "";
                }
            }
            catch
            {
            }
            return View();
        }

       

    }
}