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
    public class AuthorsController : ApiController
    {
        private dbbookEntities _context;
        public AuthorsController()
        {
            this._context = new dbbookEntities();
        }

        public List<AuthorsDto> GetAuthors()
        {
            var authors = this._context.tbl_author.ToList();
            var authorsDto = new List<AuthorsDto>();

            foreach (var author in authors)
            {
                var authorDto = new AuthorsDto(author);
                authorsDto.Add(authorDto);
            }
            return authorsDto;
        }

        public AuthorsDto GetAuthor(int iD)
        {
            var author = this._context.tbl_author.Find(iD);

            if (author == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new AuthorsDto(author);
        }

        [HttpPost]
        public AuthorsDto CreateAuthor(tbl_author author)
        {
            this._context.tbl_author.Add(author);
            this._context.SaveChanges();

            return new AuthorsDto(author);
        }

        [HttpPut]
        public AuthorsDto UpdateAuthor(tbl_author author)
        {
            var authorInDb = this._context.tbl_author.Find(author.au_id);

            if (authorInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            authorInDb.au_name = author.au_name;

            this._context.SaveChanges();

            return new AuthorsDto(authorInDb);
        }

        [HttpDelete]
        public void DeleteAuthor(int id)
        {
            var authorInDb = this._context.tbl_author.Find(id);

            if (authorInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this._context.tbl_author.Remove(authorInDb);
            this._context.SaveChanges();
        }
    }
}