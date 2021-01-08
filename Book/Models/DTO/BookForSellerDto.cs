using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Book.Models.DTO
{
    public class BookForSellerDto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookForSellerDto()
        {

        }

        public BookForSellerDto(tbl_book book)
        {
            if (book == null)
                throw new Exception("product object is null");

            this.BookID = book.book_id;
            this.Name = book.book_name;
            this.Description = book.book_description;
            this.CategoryID = (int)book.book_fk_cateid;
            this.CategoryName = book.tbl_category.cate_name;
            this.Price = (int)book.book_price;
            this.PublisherID = (int)book.book_fk_puid;
            this.PublisherName = book.tbl_publisher.pu_name;
            this.AuthorID = (int)book.book_fk_auid;
            this.AuthorName = book.tbl_author.au_name;
            this.Quantity = (int)book.book_quantity;
            this.Status = (bool)book.book_status;
            this.Image = book.book_img;
        }

        public int BookID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        
        public string Image { get; set; }

        public bool Status { get; set; }

        [Required]
        public int PublisherID { get; set; }
        public string PublisherName { get; set; }

        [Required]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        [Required]
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }


        public tbl_book CreateModel()
        {
            tbl_book book = new tbl_book();

            book.book_name = this.Name;
            book.book_description = this.Description;
            book.book_fk_cateid = this.CategoryID;
            book.book_price = this.Price;
            book.book_fk_puid = this.PublisherID;
            book.book_fk_auid = this.AuthorID;
            book.book_quantity = this.Quantity;
            book.book_status = this.Status;

            return book;
        }

        public void UpdateModel(tbl_book book)
        {
            book.book_name = this.Name;
            book.book_description = this.Description;
            book.book_fk_cateid = this.CategoryID;
            book.book_price = this.Price;
            book.book_fk_puid = this.PublisherID;
            book.book_quantity = this.Quantity;
            book.book_fk_auid = this.AuthorID;
            book.book_status = this.Status;
        }
    }
}