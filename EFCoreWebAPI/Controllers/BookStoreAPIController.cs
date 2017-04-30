using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[Route("api/TokoBuku/[action]")]
public class BookStoreAPIController : Controller {
    [HttpGet]
    public IEnumerable<string> GetAuthors()
    {
        return new string[] { "author1", "author2" };
    }

    [HttpGet]
    public IEnumerable<string> GetCategories()
    {
        return new string[] { "category1", "category2" };
    }

    [HttpGet]
    public IEnumerable<string> GetBooks()
    {
        return new string[] { "book1", "book2" };
    }
}