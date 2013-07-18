using System;
using System.Web.Mvc;
using DataAccessObjects;
using MongoLibrary;

namespace GuestBook.Controllers
{
    public class HomeController : BaseController
    {
        IGenericRepository<GuestBookEntry> _guestBookEntry;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="guestBookEntry">The guest book entry.</param>
        public HomeController(IGenericRepository<GuestBookEntry> guestBookEntry)
        {
            _guestBookEntry = guestBookEntry;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.CurrentData = DateTime.Now;
            return View();
        }

        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Contacts this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            return View();
        }
    }
}
