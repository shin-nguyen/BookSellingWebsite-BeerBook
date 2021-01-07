using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.Models.DTO
{
    public class CategoryDto
    {
        private tbl_category _category;

        public CategoryDto()
        {
            _category = new tbl_category();
        }

        public CategoryDto(tbl_category category)
        {
            if (category != null)
            {
                _category = category;
                this.CategoryID = category.cate_id;
                this.Name = category.cate_name;
               
                if (category.tbl_book.Count() == 0)
                    IsHavingBook = false;
                else
                    IsHavingBook = true;
            }
        }

        public int CategoryID { get; set; }

        public string Name { get; set; }

        public bool IsHavingBook { get; set; }

    }
}