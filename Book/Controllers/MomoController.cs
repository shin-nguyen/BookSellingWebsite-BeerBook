using Book.helper;
using Book.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Controllers
{
    public class MomoController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PaymentWithMomo()
        {
            dbbookEntities _db = new dbbookEntities();
            int userid = Convert.ToInt32(Session["user_id"]);
            List<tbl_cart> cart = _db.tbl_cart.Where(x => x.cart_fk_cusid == userid).ToList();
            double cart_total = 0;
            foreach (var item in cart)
            {
                double tmp = Convert.ToDouble(Convert.ToDouble(item.tbl_book.book_price) * Convert.ToDouble(item.cart_book_amount));
                cart_total += tmp;
            }

            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOZ8VR20210114";
            string accessKey = "1c2mi3RoQ9EY8BUC";
            string serectkey = "zW2t9QyRqZfhjOACxytP0cwzvvxUAe9R";
            string orderInfo = "Info";
            string returnUrl = "https://localhost:44397/ShoppingCart/CreateAnOrder/1";
            string notifyurl = "https://localhost:44397/ShoppingCart/CreateAnOrder/1";

            string amount = cart_total.ToString();
            string orderid = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            //log.Debug("rawHash = " + rawHash);

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);
            //log.Debug("Signature = " + signature);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };
            //log.Debug("Json request to MoMo: " + message.ToString());
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);
            //log.Debug("Return from MoMo: " + jmessage.ToString());
            //System.Diagnostics.Process.Start(jmessage.GetValue("payUrl").ToString());
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
    }
}