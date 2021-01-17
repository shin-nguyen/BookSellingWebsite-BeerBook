using Book.Models;
using Book.Models.Consts;
using Book.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Book.Controllers.Api
{
    public class OrdersController : ApiController
    {
        dbbookEntities _context;

        //public OrdersController()
        //{
        //    this._context = new dbbookEntities();
        //}
        //[HttpGet]
        //public List<OrderDto> GetListByStatus(int status)
        //{
        //    var orders = this._context.tbl_order.Where(o => o.order_stt_fk == status).ToList();

        //    var orderDtos = new List<OrderDto>();

        //    foreach (var order in orders)
        //    {
        //        orderDtos.Add(new OrderDto(order));
        //    }

        //    return orderDtos;
        //}

        //[HttpGet]
        //public List<OrderDto> GetList(int customerID)
        //{
        //    var orders = this._context.tbl_order.Where(o => o.order_fk_cusid == customerID).ToList();

        //    var orderDtos = new List<OrderDto>();

        //    foreach (var order in orders)
        //    {
        //        orderDtos.Add(new OrderDto(order));
        //    }

        //    return orderDtos;
        //}
        //[HttpGet]
        //public List<OrderDto> GetList(int customerID, DateTime orderTime)
        //{
        //    var orders = this._context.tbl_order.Where(o => o.order_fk_cusid == customerID && o.order_time == orderTime).ToList();

        //    var orderDtos = new List<OrderDto>();

        //    foreach (var order in orders)
        //    {
        //        orderDtos.Add(new OrderDto(order));
        //    }

        //    return orderDtos;
        //}
        //[HttpGet]
        //public OrderDto Get(int orderID)
        //{
        //    var order = this._context.tbl_order.Find(orderID);

        //    if (order == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    return new OrderDto(order);
        //}
        //[HttpGet]
        //public void ConfirmOrder(int orderID)
        //{
        //    var order = this._context.tbl_order.Find(orderID);

        //    if (order == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    if (order.order_stt_fk != 0)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    order.order_stt_fk = OrderStates.Confirmed;

        //    this._context.SaveChanges();
        //}

        //[HttpGet]
        //public void CancelOrder(int orderID)
        //{
        //    var order = this._context.tbl_order.Find(orderID);

        //    if (order == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    if (order.order_stt_fk != OrderStates.Pending)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    var isSuccess = new ObjectParameter("isSuccess", typeof(bool));

        //    this._context.CancelAnOrder(orderID, OrderStates.Canceled, isSuccess);

        //    if (!(bool)isSuccess.Value)
        //        throw new Exception("Failure canceling an order");
        //}
        //[HttpGet]
        //public void DeleteOrder(int orderID)
        //{
        //    var order = this._context.tbl_order.Find(orderID);

        //    if (order == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    if (order.order_stt_fk != OrderStates.Canceled)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    var isSuccess = new ObjectParameter("isSuccess", typeof(bool));

        //    this._context.DeleteAnOrder(orderID, OrderStates.Canceled, isSuccess);

        //    if (!(bool)isSuccess.Value)
        //        throw new Exception("Failure deleting an order");
        //}
    }
}