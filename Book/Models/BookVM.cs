using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book.Models
{
    public class BookVM
    {
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

        [Required]
        public int PublisherID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int AuthorID { get; set; }

        public string Image { get; set; }
        public byte[] ImageFile { get; set; }
    }
}