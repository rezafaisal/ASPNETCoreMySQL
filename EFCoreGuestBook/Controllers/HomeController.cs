using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EFCoreGuestBook.Models;

namespace EFCoreGuestBook.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            GuestBookDataContext db = new GuestBookDataContext();
            var items = db.GuestBooks.ToList();
            
            return View(items);
        }

        [HttpGet]
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public IActionResult Create(GuestBook item){
            if(ModelState.IsValid){
                GuestBookDataContext db = new GuestBookDataContext();
                db.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
