using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.Models.DTO
{
    public class PublisherDto
    {
        private tbl_publisher _publisher;

        public PublisherDto()
        {
            _publisher = new tbl_publisher();
        }

        public PublisherDto(tbl_publisher publisher)
        {
            if (publisher != null)
            {
                _publisher = publisher;
                this.PublisherID = publisher.pu_id;
                this.Name = publisher.pu_name;

                if (publisher.tbl_book.Count() == 0)
                    IsHavingBook = false;
                else
                    IsHavingBook = true;
            }
        }

        public int PublisherID { get; set; }

        public string Name { get; set; }

        public bool IsHavingBook { get; set; }
    }
}