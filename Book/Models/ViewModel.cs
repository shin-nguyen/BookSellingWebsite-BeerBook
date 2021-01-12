using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.Models
{
    public class ViewModel
    {
        private dbbookEntities _context;
        public ViewModel()
        {
            _context = new dbbookEntities();
        }
        public tbl_category category { get; set; }
        public tbl_book product { get; set; }
        public List<tbl_book> allProducts { get; set; }
        public List<tbl_book> allProductsOfProducer { get; set; }
        public List<tbl_book> allProductsOfCategory { get; set; }
        public int countCarts { get; set; }
        public List<tbl_category> allCategories()
        {
            var res = _context.Database.SqlQuery<tbl_category>("select * from view_Category_List").ToList();
            return res;
        }
        public List<tbl_book> productsOfSearch { get; set; }
    }
}
