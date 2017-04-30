using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EFCoreBookStore.Models;

namespace EFCoreBookStore.Controllers
{
    public class LatihanController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LatihanViewBag()
        {
            ViewBag.VariableInt = 13;
            ViewBag.VariableString = "ASP.NET Core MVC";
            return View();
        }

        [HttpGet]
        public IActionResult Template()
        {
            BookStoreDataContext db = new BookStoreDataContext();
            var test = db.Authors;
            ViewBag.Authors = db.Authors.Select(p => p);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Template(Category item)
        {
            if (ModelState.IsValid)
            {
            }
            return View();
        }
    }
}