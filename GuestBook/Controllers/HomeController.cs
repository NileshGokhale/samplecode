using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoLibrary;
using DTO;

namespace GuestBook.Controllers
{
    public class HomeController : BaseController
    {
        IGenericRepository<GuestBookEntry> _guestBookEntry;
        public HomeController(IGenericRepository<GuestBookEntry> guestBookEntry)
        {
            _guestBookEntry = guestBookEntry;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.CurrentData = DateTime.Now;
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
