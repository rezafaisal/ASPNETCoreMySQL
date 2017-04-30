using Microsoft.AspNetCore.Mvc;
using BelajarASPNETCoreMVC.Models;

namespace BelajarASPNETCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(GuestBook data){
            ViewBag.GuestBookMessage = "Hello " + data.Name + 
                                       "(" + data.Email + 
                                       ") menulis " + data.Message;
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
