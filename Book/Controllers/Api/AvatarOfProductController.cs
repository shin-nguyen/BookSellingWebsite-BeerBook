using Book.Models.DTO;
using Book.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Book.Controllers.Api
{
    public class AvatarOfProductController : ApiController
    {
        private dbbookEntities _context;
        public AvatarOfProductController()
        {
            this._context = new dbbookEntities();
        }

        [HttpGet]
        public HttpResponseMessage GetAvatarOfProduct(int productID)
        {
            var avatarOfProduct = this._context.tbl_avtofbook.Find(productID);

            if (avatarOfProduct == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            return GetResponseMessage(avatarOfProduct);
        }

        [HttpGet]
        public byte[] GetAvatarOfProductAsByte(int productID)
        {
            var avatarOfProduct = this._context.tbl_avtofbook.Find(productID);

            if (avatarOfProduct == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            return avatarOfProduct.avtofbook_img;
        }

        [HttpPost]
        public HttpResponseMessage Upload()
        {
            var httpPostedFile = HttpContext.Current.Request.Files["avtofbook_img"];
            var productID = HttpContext.Current.Request.Form[0];

            BinaryReader reader = new BinaryReader(httpPostedFile.InputStream);

            var image = reader.ReadBytes(httpPostedFile.ContentLength);

            var avatarOfProduct = new tbl_avtofbook();
            avatarOfProduct.avtofbook_id = int.Parse(productID);
            avatarOfProduct.avtofbook_img = image;

            this._context.tbl_avtofbook.Add(avatarOfProduct);
            this._context.SaveChanges();

            return GetResponseMessage(avatarOfProduct);
        }

        [HttpPut]
        public HttpResponseMessage Update()
        {
            var httpPostedFile = HttpContext.Current.Request.Files["avtofbook_img"];
            var productID = HttpContext.Current.Request.Form[0];

            BinaryReader reader = new BinaryReader(httpPostedFile.InputStream);

            var image = reader.ReadBytes(httpPostedFile.ContentLength);

            var avatarOfProductInDb = this._context.tbl_avtofbook.Find(int.Parse(productID));
            if (avatarOfProductInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            avatarOfProductInDb.avtofbook_img = image;
            this._context.SaveChanges();

            return GetResponseMessage(avatarOfProductInDb);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int productID)
        {
            var avatarOfProduct = this._context.tbl_avtofbook.Find(productID);

            if (avatarOfProduct == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this._context.tbl_avtofbook.Remove(avatarOfProduct);
            this._context.SaveChanges();

            return GetResponseMessage(avatarOfProduct);
        }

        private HttpResponseMessage GetResponseMessage(tbl_avtofbook avatarOfProduct)
        {
            MemoryStream ms = new MemoryStream(avatarOfProduct.avtofbook_img);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = new StreamContent(ms);

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

            return response;
        }
    }
}
