using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Book.Models;
using Book.Models.DTO;

namespace Book.Controllers.API
{
    public class CategoriesController : ApiController
    {
        private dbbookEntities _context;
        public CategoriesController()
        {
            this._context = new dbbookEntities();
        }

        public List<CategoryDto> GetCategories()
        {
            var categories = this._context.tbl_category.ToList();
            var categoriesDto = new List<CategoryDto>();

            foreach (var category in categories)
            {
                var categoryDto = new CategoryDto(category);
                categoriesDto.Add(categoryDto);
            }
            return categoriesDto;
        }

        public CategoryDto GetCategory(int iD)
        {
            var category = this._context.tbl_category.Find(iD);

            if (category == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new CategoryDto(category);
        }

        [HttpPost]
        public CategoryDto CreateCategory(tbl_category category)
        {
            this._context.tbl_category.Add(category);
            this._context.SaveChanges();

            return new CategoryDto(category);
        }

        [HttpPut]
        public CategoryDto UpdateCategory(tbl_category category)
        {
            var categoryInDb = this._context.tbl_category.Find(category.cate_id);

            if (categoryInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            categoryInDb.cate_name = category.cate_name;

            this._context.SaveChanges();

            return new CategoryDto(categoryInDb);
        }

        [HttpDelete]
        public void DeleteCategory(int id)
        {
            var categoryInDb = this._context.tbl_category.Find(id);

            if (categoryInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this._context.tbl_category.Remove(categoryInDb);
            this._context.SaveChanges();
        }
    }
}