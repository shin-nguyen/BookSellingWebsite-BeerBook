using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Book.Models;

namespace Book.Controllers.Api
{
    public class AvatarOfBookController : ApiController
    {
        private dbbookEntities _context;
        public AvatarOfBookController()
        {
            this._context = new dbbookEntities();
        }

        [HttpGet]
        public HttpResponseMessage GetAvatarOfBook(int bookID)
        {
            var avatarOfBook = this._context.tbl_avtofbook.Where(x=>x.avtofbook_id==bookID).SingleOrDefault();

            if (avatarOfBook == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            return GetResponseMessage(avatarOfBook);
        }

        [HttpGet]
        public byte[] GetAvatarOfBookAsByte(int bookID)
        {
            var avatarOfBook = this._context.tbl_avtofbook.Where(x => x.avtofbook_id == bookID).SingleOrDefault();

            if (avatarOfBook == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            return avatarOfBook.avtofbook_img;
        }

        [HttpPost]
        public HttpResponseMessage Upload()
        {
            var httpPostedFile = HttpContext.Current.Request.Files["imageFile"];
            var bookID = HttpContext.Current.Request.Form[0];

            BinaryReader reader = new BinaryReader(httpPostedFile.InputStream);

            var image = reader.ReadBytes(httpPostedFile.ContentLength);

            var avatarOfBook = new tbl_avtofbook();
            avatarOfBook.avtofbook_id = int.Parse(bookID);
            avatarOfBook.avtofbook_img = image;

            this._context.tbl_avtofbook.Add(avatarOfBook);
            this._context.SaveChanges();

            return GetResponseMessage(avatarOfBook);
        }

        [HttpPut]
        public HttpResponseMessage Update()
        {
            var httpPostedFile = HttpContext.Current.Request.Files["imageFile"];
            var bookID = HttpContext.Current.Request.Form[0];

            BinaryReader reader = new BinaryReader(httpPostedFile.InputStream);

            var image = reader.ReadBytes(httpPostedFile.ContentLength);

            var avatarOfBookInDb = this._context.tbl_avtofbook.Find(int.Parse(bookID));
            if (avatarOfBookInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            avatarOfBookInDb.avtofbook_img = image;
            this._context.SaveChanges();

            return GetResponseMessage(avatarOfBookInDb);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int bookID)
        {
            var avatarOfBook = this._context.tbl_avtofbook.Find(bookID);

            if (avatarOfBook == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this._context.tbl_avtofbook.Remove(avatarOfBook);
            this._context.SaveChanges();

            return GetResponseMessage(avatarOfBook);
        }

        private HttpResponseMessage GetResponseMessage(tbl_avtofbook avatarOfBook)
        {
            MemoryStream ms = new MemoryStream(avatarOfBook.avtofbook_img);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = new StreamContent(ms);

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

            return response;
        }
    }
}