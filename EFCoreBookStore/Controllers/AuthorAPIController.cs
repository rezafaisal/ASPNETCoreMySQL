using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EFCoreBookStore.Models;

namespace EFCoreBookStore.Controllers{
    [Route("api/[controller]")]
    public class AuthorAPI : Controller{
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            BookStoreDataContext db = new BookStoreDataContext();
            var items = db.Authors.ToList();

            return items;
        }

        // GET api/<controller>/1
        [HttpGet("{id}", Name = "GetAuthor")]
        public Author Get(int id)
        {
            BookStoreDataContext db = new BookStoreDataContext();
            var item = db.Authors.Where(p => p.AuthorID.Equals(id)).Single<Author>();

            return item;
        }
    }
}