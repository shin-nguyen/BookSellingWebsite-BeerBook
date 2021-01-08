﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book.Models;

namespace Book.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        private dbbookEntities _context;

        public BookController()
        {
            this._context = new dbbookEntities();
        }

        [HttpGet]
        public ActionResult AddNewBook()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<tbl_category> li0 = _context.tbl_category.ToList();
            List<tbl_author> li1 = _context.tbl_author.ToList();
            List<tbl_publisher> li2 = _context.tbl_publisher.ToList();

            ViewBag.listcate = new SelectList(li0, "cate_id", "cate_name");
            ViewBag.listau = new SelectList(li1, "au_id", "au_name");
            ViewBag.listpu = new SelectList(li2, "pu_id", "pu_name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewBook([Bind(Exclude = "imageFile")] BookVM bookVM, HttpPostedFileBase imageFile, string description)
        {
            List<tbl_category> li0 = _context.tbl_category.ToList();
            List<tbl_author> li1 = _context.tbl_author.ToList();
            List<tbl_publisher> li2 = _context.tbl_publisher.ToList();

            ViewBag.listcate = new SelectList(li0, "cate_id", "cate_name");
            ViewBag.listau = new SelectList(li1, "au_id", "au_name");
            ViewBag.listpu = new SelectList(li2, "pu_id", "pu_name");

            tbl_book ob = _context.tbl_book.Where(x => x.book_name == bookVM.Name).SingleOrDefault();
            if (ob != null)
            {
                ViewBag.error = "This book already exists!";
            }
            else
            {
                bookVM.Description = description;

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string strExtexsion = Path.GetExtension(imageFile.FileName).Trim();

                    if (strExtexsion != ".jpg")
                    {
                        ViewBag.error = "Required .jpg file!";
                        return View();
                    }

                    string fileName = System.IO.Path.GetFileName(imageFile.FileName);
                    string UrlImage = Server.MapPath("~/Assets/images/" + fileName);
                    imageFile.SaveAs(UrlImage);
                    bookVM.Image = fileName;

                }
                else
                {
                    ViewBag.error = "Required .jpg file!";
                    return View();
                }


                if (ModelState.IsValid)
                {
                    tbl_book nb = new tbl_book();
                    nb.book_name = bookVM.Name;
                    nb.book_description = bookVM.Description;
                    nb.book_fk_auid = bookVM.AuthorID;
                    nb.book_fk_cateid = bookVM.CategoryID;
                    nb.book_fk_puid = bookVM.PublisherID;
                    nb.book_price = bookVM.Price;
                    nb.book_quantity = bookVM.Quantity;
                    nb.book_status = true;
                    nb.book_img = bookVM.Image;
                    _context.tbl_book.Add(nb);
                    _context.SaveChanges();

                    

                    ViewBag.success = "";
                }
                else
                {
                    ViewBag.error = "";
                }

            }

            return View();
        }

    }
}