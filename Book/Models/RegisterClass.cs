using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Book.Models
{
    public class RegisterClass
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [RegularExpression(@"[0-9]{6,12}")]
        public string Phone { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 6)]
        public string Address { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}