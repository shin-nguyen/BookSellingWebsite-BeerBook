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

        
    }
}