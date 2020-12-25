using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book.Models;

namespace Book.Controllers
{
    public class AdminController : Controller
    {
        //db
        dbbookEntities db = new dbbookEntities();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        //Category-------------------------------------------------------------------------------------------------------------------------------
        //ShowCategory
        [HttpGet]
        public ActionResult ShowCategory()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<tbl_category> li = db.tbl_category.OrderBy(x => x.cate_id).ToList();
            ViewData["list"] = li;
            return View();
        }
        [HttpPost]
        public ActionResult ShowCategory(string search)
        {
            if (search == null)
            {
                return RedirectToAction("ShowCategory");
            }

            List<tbl_category> li = db.tbl_category.Where(x => x.cate_name.Contains(search)).OrderBy(x => x.cate_id).ToList();
            ViewData["list"] = li;
            return View();
        }

        //ShowCategory//AddCategory
        [HttpGet]
        public ActionResult AddCategory()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(tbl_category c)
        {
            tbl_category cg = db.tbl_category.Where(x => x.cate_name == c.cate_name).SingleOrDefault();

            if (cg == null)
            {
                if (ModelState.IsValid)
                {
                    tbl_category cate = new tbl_category();
                    cate.cate_name = c.cate_name;
                    db.tbl_category.Add(cate);
                    db.SaveChanges();

                    ViewBag.msg1 = "This category has been added!";
                }
                else
                {
                    ViewBag.msg = "";
                }
            }
            else
            {
                ViewBag.msg = "This category already exists!";
            }
            return View();
        }

        //ShowCategory//BookOfCategory
        public ActionResult BookOfCategory(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                tbl_category cate = db.tbl_category.Where(x => x.cate_id == id).SingleOrDefault();
                ViewBag.cate = cate.cate_name.ToString();
                List<tbl_book> li = db.tbl_book.Where(x => x.book_fk_cateid == id).ToList();
                return View(li);
            }
        }

        //ShowCategory//EditCategory
        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                tbl_category cate = db.tbl_category.Where(x => x.cate_id == id).SingleOrDefault();
                return View(cate);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(tbl_category c, int? id)
        {
            tbl_category cg = db.tbl_category.Where(x => x.cate_name == c.cate_name && x.cate_id!=id).SingleOrDefault();
            if (cg == null)
            {
                if (ModelState.IsValid)
                {
                    tbl_category cate = db.tbl_category.Where(x => x.cate_id == id).SingleOrDefault();
                    cate.cate_name = c.cate_name;
                    db.tbl_category.AddOrUpdate(cate);
                    db.SaveChanges();

                    ViewBag.msg1 = "This category had been edited!";
                }
                else
                {
                    ViewBag.msg = "";
                }
            }
            else
            {
                ViewBag.msg = "This category already exists!";
            }

            tbl_category ct = db.tbl_category.Where(x => x.cate_id == id).SingleOrDefault();
            return View(ct);
        }

        //ShowCategory//DeleteCategory
        public ActionResult DeleteCategory(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                tbl_category cate = db.tbl_category.Where(x => x.cate_id == id).SingleOrDefault();
                db.tbl_category.Remove(cate);
                db.SaveChanges();
            }
            catch { }

            return RedirectToAction("ShowCategory");
        }
        //Category-------------------------------------------------------------------------------------------------------------------------------

        //Publisher------------------------------------------------------------------------------------------------------------------------------
        //ShowPublisher
        [HttpGet]
        public ActionResult ShowPublisher()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<tbl_publisher> li = db.tbl_publisher.OrderBy(x => x.pu_id).ToList();
            ViewData["list"] = li;
            return View();
        }
        [HttpPost]
        public ActionResult ShowPublisher(string search)
        {
            if (search == null)
            {
                return RedirectToAction("ShowPublisher");
            }

            List<tbl_publisher> li = db.tbl_publisher.Where(x => x.pu_name.Contains(search)).OrderBy(x => x.pu_id).ToList();
            ViewData["list"] = li;
            return View();
        }

        //ShowPublisher//AddPublisher
        [HttpGet]
        public ActionResult AddPublisher()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPublisher(tbl_publisher p, string description)
        {
            tbl_publisher pb = db.tbl_publisher.Where(x => x.pu_name == p.pu_name).SingleOrDefault();

            if (pb == null)
            {
                if (ModelState.IsValid)
                {
                    tbl_publisher publisher = new tbl_publisher();
                    publisher.pu_name = p.pu_name;
                    publisher.pu_description = description;
                    db.tbl_publisher.Add(publisher);
                    db.SaveChanges();

                    ViewBag.msg1 = "This publisher has been added!";
                }
                else
                {
                    ViewBag.msg = "";
                }
            }
            else
            {
                ViewBag.msg = "This publisher already exists!";
            }
            return View();
        }

        //ShowPublisher//BookOfPublisher
        public ActionResult BookOfPublisher(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                tbl_publisher pu = db.tbl_publisher.Where(x => x.pu_id == id).SingleOrDefault();
                ViewBag.pu = pu.pu_name.ToString();
                List<tbl_book> li = db.tbl_book.Where(x => x.book_fk_puid == id).ToList();
                return View(li);
            }
        }

        //ShowPublisher//EditPublisher
        [HttpGet]
        public ActionResult EditPublisher(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                tbl_publisher pu = db.tbl_publisher.Where(x => x.pu_id == id).SingleOrDefault();
                TempData["description"] = pu.pu_description.ToString();
                TempData.Keep();
                return View(pu);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPublisher(tbl_publisher p, int? id, string description)
        {
            tbl_publisher pu = db.tbl_publisher.Where(x => x.pu_name == p.pu_name && x.pu_id!=id).SingleOrDefault();
            if (pu == null)
            {
                if (ModelState.IsValid)
                {
                    tbl_publisher publisher = db.tbl_publisher.Where(x => x.pu_id == id).SingleOrDefault();
                    publisher.pu_name = p.pu_name;
                    publisher.pu_description = description;
                    db.tbl_publisher.AddOrUpdate(publisher);
                    db.SaveChanges();

                    ViewBag.msg1 = "This publisher had been edited!";
                }
                else
                {
                    ViewBag.msg = "";
                }
            }
            else
            {
                ViewBag.msg = "This publisher already exists!";
            }

            tbl_publisher pb = db.tbl_publisher.Where(x => x.pu_id == id).SingleOrDefault();
            TempData["description"] = pb.pu_description.ToString();
            TempData.Keep();
            return View(pb);
        }

        //ShowPublisher//DeletePublisher
        public ActionResult DeletePublisher(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                tbl_publisher pu = db.tbl_publisher.Where(x => x.pu_id == id).SingleOrDefault();
                db.tbl_publisher.Remove(pu);
                db.SaveChanges();
            }
            catch { }

            return RedirectToAction("ShowPublisher");
        }
        //Publisher------------------------------------------------------------------------------------------------------------------------------

        //Author------------------------------------------------------------------------------------------------------------------------------
        //ShowAuthor
        [HttpGet]
        public ActionResult ShowAuthor()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<tbl_author> li = db.tbl_author.OrderBy(x => x.au_id).ToList();
            ViewData["list"] = li;
            return View();
        }
        [HttpPost]
        public ActionResult ShowAuthor(string search)
        {
            if (search == null)
            {
                return RedirectToAction("ShowAuthor");
            }

            List<tbl_author> li = db.tbl_author.Where(x => x.au_name.Contains(search)).OrderBy(x => x.au_id).ToList();
            ViewData["list"] = li;
            return View();
        }

        //ShowAuthor//AddAuthor
        [HttpGet]
        public ActionResult AddAuthor()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAuthor(tbl_author a, string description)
        {
            tbl_author au = db.tbl_author.Where(x => x.au_name == a.au_name).SingleOrDefault();

            if (au == null)
            {
                if (ModelState.IsValid)
                {
                    tbl_author author = new tbl_author();
                    author.au_name = a.au_name;
                    author.au_description = description;
                    db.tbl_author.Add(author);
                    db.SaveChanges();

                    ViewBag.msg1 = "This author has been added!";
                }
                else
                {
                    ViewBag.msg = "";
                }
            }
            else
            {
                ViewBag.msg = "This author already exists!";
            }
            return View();
        }

        //ShowAuthor//BookOfAuthor
        public ActionResult BookOfAuthor(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                tbl_author au = db.tbl_author.Where(x => x.au_id == id).SingleOrDefault();
                ViewBag.au = au.au_name.ToString();
                List<tbl_book> li = db.tbl_book.Where(x => x.book_fk_auid == id).ToList();
                return View(li);
            }
        }

        //ShowAuthor//EditAuthor
        [HttpGet]
        public ActionResult EditAuthor(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                tbl_author au = db.tbl_author.Where(x => x.au_id == id).SingleOrDefault();
                TempData["description"] = au.au_description.ToString();
                TempData.Keep();
                return View(au);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuthor(tbl_author a, int? id, string description)
        {
            tbl_author au = db.tbl_author.Where(x => x.au_name == a.au_name && x.au_id != id).SingleOrDefault();
            if (au == null)
            {
                if (ModelState.IsValid)
                {
                    tbl_author publisher = db.tbl_author.Where(x => x.au_id == id).SingleOrDefault();
                    publisher.au_name = a.au_name;
                    publisher.au_description = description;
                    db.tbl_author.AddOrUpdate(publisher);
                    db.SaveChanges();

                    ViewBag.msg1 = "This author had been edited!";
                }
                else
                {
                    ViewBag.msg = "";
                }
            }
            else
            {
                ViewBag.msg = "This author already exists!";
            }

            tbl_author at = db.tbl_author.Where(x => x.au_id == id).SingleOrDefault();
            TempData["description"] = at.au_description.ToString();
            TempData.Keep();
            return View(at);
        }

        //ShowAutho//DeleteAuthor
        public ActionResult DeleteAuthor(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                tbl_author au = db.tbl_author.Where(x => x.au_id == id).SingleOrDefault();
                db.tbl_author.Remove(au);
                db.SaveChanges();
            }
            catch { }

            return RedirectToAction("ShowAuthor");
        }
        //Author------------------------------------------------------------------------------------------------------------------------------


        //Book--------------------------------------------------------------------------------------------------------------------------------
        //ShowBook
        [HttpGet]
        public ActionResult ShowBook()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<tbl_book> li = db.tbl_book.OrderBy(x => x.book_id).ToList();
            return View(li);
        }
        [HttpPost]
        public ActionResult ShowBook(string search)
        {
            if (search == null)
            {
                return RedirectToAction("ShowBook");
            }

            List<tbl_book> li = db.tbl_book.Where(x => x.book_name.Contains(search)).OrderBy(x => x.book_id).ToList();
            return View(li);
        }
        //ShowBook//AddBook
        [HttpGet]
        public ActionResult AddBook()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<tbl_category> li1 = db.tbl_category.ToList();
            List<tbl_publisher> li2 = db.tbl_publisher.ToList();
            List<tbl_author> li3 = db.tbl_author.ToList();
            ViewBag.li1 = new SelectList(li1, "cate_id", "cate_name");
            ViewBag.li2 = new SelectList(li2, "pu_id", "pu_name");
            ViewBag.li3 = new SelectList(li3, "au_id", "au_name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(tbl_book b, HttpPostedFileBase img, string description)
        {
            tbl_book bk = db.tbl_book.Where(x => x.book_name == b.book_name).SingleOrDefault();

            List<tbl_category> li1 = db.tbl_category.ToList();
            List<tbl_publisher> li2 = db.tbl_publisher.ToList();
            List<tbl_author> li3 = db.tbl_author.ToList();
            ViewBag.li1 = new SelectList(li1, "cate_id", "cate_name");
            ViewBag.li2 = new SelectList(li2, "pu_id", "pu_name");
            ViewBag.li3 = new SelectList(li3, "au_id", "au_name");

            if (bk == null)
            {
                if (ModelState.IsValid)
                {
                    tbl_book book = new tbl_book();
                    book.book_name = b.book_name;
                    book.book_description = description;
                    book.book_price = b.book_price;
                    book.book_fk_auid = b.book_fk_auid;
                    book.book_fk_cateid = b.book_fk_cateid;
                    book.book_fk_puid = b.book_fk_puid;
                    book.book_quantity = b.book_quantity;

                    if (img != null && System.IO.Path.GetFileName(img.FileName) != "")
                    {
                        string extension = System.IO.Path.GetExtension(img.FileName);
                        if (extension == ".jpg" || extension == ".png")
                        {
                            string filename = System.IO.Path.GetFileName(img.FileName);
                            string urlImage = Server.MapPath("~/Assets/images/" + filename);
                            img.SaveAs(urlImage);

                            book.book_img = filename;
                        }
                        else
                        {
                            ViewBag.msg = "File image is invalid!";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Please upload an image!";
                        return View();
                    }

                    db.tbl_book.Add(book);
                    db.SaveChanges();

                    ViewBag.msg1 = "This book has been added!";
                }
                else
                {
                    ViewBag.msg = "";
                }
            }
            else
            {
                ViewBag.msg = "This book already exists!";
            }
            return View();
        }
        //ShowBook//EditBook
        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return RedirectToAction("ShowBook");
            }

            List<tbl_category> li1 = db.tbl_category.ToList();
            List<tbl_publisher> li2 = db.tbl_publisher.ToList();
            List<tbl_author> li3 = db.tbl_author.ToList();
            ViewBag.li1 = new SelectList(li1, "cate_id", "cate_name");
            ViewBag.li2 = new SelectList(li2, "pu_id", "pu_name");
            ViewBag.li3 = new SelectList(li3, "au_id", "au_name");

            tbl_book b = db.tbl_book.Where(x => x.book_id == id).SingleOrDefault();

            return View(b);
        }
        [HttpPost]
        public ActionResult EditBook(tbl_book b, int? id, string description, HttpPostedFileBase img)
        {
            tbl_book bk = db.tbl_book.Where(x => x.book_name == b.book_name && x.book_id != id).SingleOrDefault();

            List<tbl_category> li1 = db.tbl_category.ToList();
            List<tbl_publisher> li2 = db.tbl_publisher.ToList();
            List<tbl_author> li3 = db.tbl_author.ToList();
            ViewBag.li1 = new SelectList(li1, "cate_id", "cate_name");
            ViewBag.li2 = new SelectList(li2, "pu_id", "pu_name");
            ViewBag.li3 = new SelectList(li3, "au_id", "au_name");

            if (bk== null)
            {
                tbl_book book = db.tbl_book.Where(x => x.book_id == id).SingleOrDefault();
                
                if (img != null && System.IO.Path.GetFileName(img.FileName) != "")
                {
                    string extension = System.IO.Path.GetExtension(img.FileName);
                    if (extension == ".jpg" || extension == ".png")
                    {
                        string filename = System.IO.Path.GetFileName(img.FileName);
                        string urlImage = Server.MapPath("~/Assets/images/" + filename);
                        img.SaveAs(urlImage);

                        book.book_img = filename;
                    }
                    else
                    {
                        ViewBag.msg = "File image is invalid!";
                        return View(book);
                    }
                }

                book.book_name = b.book_name;
                book.book_price = b.book_price;
                book.book_quantity = b.book_quantity;
                book.book_description = description;
                book.book_fk_auid = b.book_fk_auid;
                book.book_fk_cateid = b.book_fk_cateid;
                book.book_fk_puid = b.book_fk_puid;
                db.tbl_book.AddOrUpdate(book);
                db.SaveChanges();
                ViewBag.msg1 = "This book had been editted!";
            }

            tbl_book bo = db.tbl_book.Where(x => x.book_id == id).SingleOrDefault();
            return View(bo);
        }
        //ShowBook//DeleteBook
        public ActionResult DeleteBook(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("ShowBook");
            }

            try
            {
                tbl_book bk = db.tbl_book.Where(x => x.book_id == id).SingleOrDefault();
                db.tbl_book.Remove(bk);
                db.SaveChanges();
            }
            catch { }

            return RedirectToAction("ShowBook");
        }


        //Book--------------------------------------------------------------------------------------------------------------------------------

    }
}