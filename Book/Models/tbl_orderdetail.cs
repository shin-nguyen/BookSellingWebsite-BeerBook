//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Book.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_orderdetail
    {
        public int order_fk_orderid { get; set; }
        public Nullable<int> order_fk_bookid { get; set; }
        public Nullable<int> order_book_amount { get; set; }
    
        public virtual tbl_book tbl_book { get; set; }
        public virtual tbl_order tbl_order { get; set; }
    }
}
