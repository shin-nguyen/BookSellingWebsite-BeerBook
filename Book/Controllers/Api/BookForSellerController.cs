using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Book.Models;
using Book.Models.DTO;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace Book.Controllers.Api
{
    public class BookForSellerController : ApiController
    {
        private dbbookEntities _context;
        public BookForSellerController()
        {
            this._context = new dbbookEntities();
        }

        public List<BookForSellerDto> GetAll()
        {
            var bookForSellersDto = new List<BookForSellerDto>();

            foreach (var book in this._context.tbl_book.ToList())
            {
                bookForSellersDto.Add(new BookForSellerDto(book));
            }

            return bookForSellersDto;
        }

        public BookForSellerDto Get(int bookID)
        {
            var book = this._context.tbl_book.Find(bookID);

            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new BookForSellerDto(book);
        }

        [System.Web.Http.HttpGet]
        public bool ModifyStatus(int bookID, bool status)
        {
            var book = this._context.tbl_book.Find(bookID);

            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            book.book_status = status;
            this._context.SaveChanges();

            return status;
        }

        [System.Web.Http.HttpPost]
        public BookForSellerDto Create(BookForSellerDto BookForSellerDto)
        {
            var book = BookForSellerDto.CreateModel();

            this._context.tbl_book.Add(book);
            this._context.SaveChanges();

            var bookInDb = this._context.tbl_book.Include(p => p.Category)
                                                    .Include(p => p.Producer)
                                                    .SingleOrDefault(p => p.BookID == book.BookID);

            return new BookForSellerDto(bookInDb);
        }

        [System.Web.Http.HttpPost]
        public BookForSellerDto CreateBookFullInfo()
        {
            var bookDto = new BookDto();

            var form = HttpContext.Current.Request.Form;

            //Get data
            bookDto.Name = form["name"].ToString();
            bookDto.Description = form["description"].ToString();
            bookDto.AuthorID = int.Parse(form["author"].ToString());
            bookDto.PublisherID = int.Parse(form["producerID"].ToString());
            bookDto.CategoryID = int.Parse(form["categoryID"].ToString());
            bookDto.Price = int.Parse(form["price"].ToString());
            bookDto.Quantity = int.Parse(form["quantity"].ToString());
            bookDto.Status = Boolean.Parse(form["status"]);

            //Get image file
            var httpPostedFile = HttpContext.Current.Request.Files["imageFile"];
            BinaryReader reader = new BinaryReader(httpPostedFile.InputStream);
            var imageFile = reader.ReadBytes(httpPostedFile.ContentLength);
            bookDto.ImageFile = imageFile;

            try
            {
                var newbook = new tbl_book();
                newbook.book_name = bookDto.Name;
                newbook.book_description = bookDto.Description;
                newbook.book_fk_puid = bookDto.PublisherID;
                newbook.book_fk_auid = bookDto.AuthorID;
                newbook.book_fk_cateid = bookDto.CategoryID;
                newbook.book_price = bookDto.Price;
                newbook.book_status = bookDto.Status;

                _context.tbl_book.Add(newbook);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Failure inserting sequence of data");
            }  

            var book = this._context.tbl_book.SingleOrDefault(p => p.Name == bookDto.Name);

            if (book == null)
                throw new Exception("Not found");
            else
            {
                var avtofbook = new tbl_avtofbook();
                avtofbook.avtofbook_id = book.book_id;
                avtofbook.avtofbook_img = bookDto.ImageFile;
                _context.tbl_avtofbook.Add(avtofbook);
                _context.SaveChanges();
            }

            return new BookForSellerDto(book);
        }
         
        [System.Web.Http.HttpPut]
        public BookForSellerDto Update(BookForSellerDto BookForSellerDto)
        {
            var book = this._context.tbl_book.Find(BookForSellerDto.BookID);

            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            BookForSellerDto.UpdateModel(book);

            this._context.SaveChanges();

            var bookInDb = this._context.tbl_book.Include(p => p.Category)
                                                    .Include(p => p.Producer)
                                                    .SingleOrDefault(p => p.BookID == book.BookID);

            return new BookForSellerDto(bookInDb);
        }

        public BookForSellerDto DeleteFullInfo(int bookID)
        {
            var book = this._context.tbl_book.Include(p => p.Category)
                                                .Include(p => p.Producer)
                                                .SingleOrDefault(p => p.book_id == bookID);
            try
            {
                var avtofbook = this._context.tbl_avtofbook.Where(x => x.avtofbook_id == book.book_id).SingleOrDefault();
                this._context.tbl_avtofbook.Remove(avtofbook);
            }
            catch
            {
                throw new Exception("Failure deleting book");
            }

            return new BookForSellerDto(book);
        }

        [System.Web.Http.HttpDelete]
        public BookForSellerDto Delete(int bookID)
        {
            var book = this._context.tbl_book.Include(p => p.Producer)
                                                .Include(p => p.Category)
                                                .SingleOrDefault(p => p.book_id == bookID);

            var bookDto = new BookForSellerDto(book);

            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this._context.tbl_book.Remove(book); // after deletion, this book no longer has category and producer
            this._context.SaveChanges();

            return bookDto;
        }
    }
}