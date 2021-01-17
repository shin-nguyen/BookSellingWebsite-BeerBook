using Book.helper;
using Book.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Controllers
{
    public class PaypalController : Controller
    {
        private Payment payment;
        double exchange_rate = 23000;

        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithPaypal()
        {
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class
                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";
                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters
                    // from the previous call to the function Create
                    // Executing a payment
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error" + ex.Message);
                //return View("FailureView");
            }
            return  RedirectToAction("CreateAnOrder", "ShoppingCart", new { id = 1 });
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
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

            var itemList = new ItemList() { items = new List<Item>() };

            //Các giá trị bao gồm danh sách sản phẩm, thông tin đơn hàng
            //Sẽ được thay đổi bằng hành vi thao tác mua hàng trên website
            foreach (var item in cart)
            {
                var item_name = item.tbl_book.book_name;
                var item_quantity = item.cart_book_amount.ToString();
                var item_price = Math.Round(Convert.ToDouble(item.tbl_book.book_price) / exchange_rate, 2).ToString();
                itemList.items.Add(new Item()
                {
                    //Thông tin đơn hàng
                    name = item_name,
                    currency = "USD",
                    price = item_price,
                    quantity = item_quantity,
                    sku = "sku"
                });
            }

            //Hình thức thanh toán qua paypal
            var payer = new Payer() { payment_method = "paypal" };
            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            //các thông tin trong đơn hàng
            var subtotal = Math.Round(cart_total / exchange_rate, 2).ToString();
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = subtotal
            };
            //Đơn vị tiền tệ và tổng đơn hàng cần thanh toán
            var total = (Convert.ToDouble(details.subtotal) + Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping)).ToString();
            var amount = new Amount()
            {
                currency = "USD",
                total = total, // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };
            var transactionList = new List<Transaction>();
            //Tất cả thông tin thanh toán cần đưa vào transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = "your invoice number",
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }
    }
}