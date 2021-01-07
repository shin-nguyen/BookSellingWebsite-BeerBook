using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Book.Models;
using Book.Models.DTO;

namespace Book.Controllers.Api
{
    public class PublishersController : ApiController
    {
        private dbbookEntities _context;
        public PublishersController()
        {
            this._context = new dbbookEntities();
        }

        public List<PublisherDto> GetPublishers()
        {
            var publishers = this._context.tbl_publisher.ToList();
            var publishersDto = new List<PublisherDto>();

            foreach (var publisher in publishers)
            {
                var publisherDto = new PublisherDto(publisher);
                publishersDto.Add(publisherDto);
            }
            return publishersDto;
        }

        public PublisherDto GetPublisher(int iD)
        {
            var publisher = this._context.tbl_publisher.Find(iD);

            if (publisher == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new PublisherDto(publisher);
        }

        [HttpPost]
        public PublisherDto CreatePublisher(tbl_publisher publisher)
        {
            this._context.tbl_publisher.Add(publisher);
            this._context.SaveChanges();

            return new PublisherDto(publisher);
        }

        [HttpPut]
        public PublisherDto UpdatePublisher(tbl_publisher publisher)
        {
            var publisherInDb = this._context.tbl_publisher.Find(publisher.pu_id);

            if (publisherInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            publisherInDb.pu_name = publisher.pu_name;
            this._context.SaveChanges();

            return new PublisherDto(publisherInDb);
        }

        [HttpDelete]
        public void DeletePublisher(int id)
        {
            var publisherInDb = this._context.tbl_publisher.Find(id);

            if (publisherInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this._context.tbl_publisher.Remove(publisherInDb);
            this._context.SaveChanges();
        }
    }
}