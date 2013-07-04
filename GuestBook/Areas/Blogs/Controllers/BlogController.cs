using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.Contracts;

using MongoLibrary;
using DTO;
namespace GuestBook.Areas.Blogs.Controllers
{
    public class BlogController : Controller
    {
        IGenericRepository<Blog> _blogRepository;

        public BlogController(IGenericRepository<Blog> _blogRepository)
        {
            this._blogRepository = _blogRepository;
        }

        //
        // GET: /Blogs/Blog/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Archive(string year, string month, int? dt)
        {
            if (!string.IsNullOrEmpty(year))
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog model)
        {
            return View();
        }
    }
}
