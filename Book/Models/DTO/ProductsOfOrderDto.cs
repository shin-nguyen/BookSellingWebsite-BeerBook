using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.Models.DTO
{
    public class ProductsOfOrderDto
    {
        public ProductsOfOrderDto()
        {
        }

        public ProductsOfOrderDto(tbl_orderdetail productsOfOrder)
        {
            this.OrderID = productsOfOrder.order_fk_orderid;
            this.ProductID =Convert.ToInt32(productsOfOrder.order_fk_bookid);
            this.amount = productsOfOrder.order_book_amount;
        }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> amount { get; set; }
    }
}