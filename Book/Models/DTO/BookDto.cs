using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book.Models.DTO
{
    public class BookDto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookDto()
        {

        }

        public int BookID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        public string Image { get; set; }

        [Required]
        public int PublisherID { get; set; }
        public string PublisherName { get; set; }

        [Required]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        [Required]
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }

        [Required]
        public byte[] ImageFile { get; set; }

        public bool InOrderOrCart { get; set; }
    }
}