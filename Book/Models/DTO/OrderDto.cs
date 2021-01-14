using Book.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book.Models.DTO
{
    public class OrderDto
    {
        public OrderDto()
        {
            this.ProductOfOrderDtos = new List<ProductsOfOrderDto>();
        }
        public OrderDto(tbl_order order)
        {
            this.OrderID = order.order_id;
            this.CustomerID = Convert.ToInt32(order.order_fk_cusid);
            this.OrderTime = Convert.ToDateTime(order.order_time);
            this.Status = Convert.ToInt32(order.tbl_status);

            this.ProductOfOrderDtos = new List<ProductsOfOrderDto>();
            foreach (var product in order.tbl_oderdetail)
            {
                this.ProductOfOrderDtos.Add(new ProductsOfOrderDto(product));
            }

            var totalOrderCost = new dbbookEntities().SPGetTotalOrderCost(order.order_id);

            this.TotalOrderCost = totalOrderCost.ToArray()[0];

        }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public System.DateTime OrderTime { get; set; }
        public Nullable<int> Status { get; set; }
        public List<ProductsOfOrderDto> ProductOfOrderDtos { get; set; }
        public int? TotalOrderCost { get; set; }
    }
}