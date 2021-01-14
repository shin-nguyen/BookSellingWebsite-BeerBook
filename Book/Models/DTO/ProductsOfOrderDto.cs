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

        public ProductsOfOrderDto(tbl_oderdetail productsOfOrder)
        {
            this.OrderID = productsOfOrder.od_fk_orderid;
            this.ProductID =Convert.ToInt32(productsOfOrder.od_fk_bookid);
            this.amount = productsOfOrder.od_book_amount;
        }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> amount { get; set; }
    }
}