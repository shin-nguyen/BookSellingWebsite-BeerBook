using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.Models.DTO
{
    public class AuthorsDto
    {
        private tbl_author _author;

        public AuthorsDto()
        {
            _author = new tbl_author();
        }

        public AuthorsDto(tbl_author author)
        {
            if (author != null)
            {
                _author = author;
                this.AuthorID = author.au_id;
                this.Name = author.au_name;

                if (author.tbl_book.Count() == 0)
                    IsHavingBook = false;
                else
                    IsHavingBook = true;
            }
        }

        public int AuthorID { get; set; }

        public string Name { get; set; }

        public bool IsHavingBook { get; set; }
    }
}