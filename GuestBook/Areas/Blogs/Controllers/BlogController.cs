using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.Contracts;

using MongoLibrary;
using DTO;
using GuestBook.Security;
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
            List<Blog> blogs = new List<Blog>();
            if (!string.IsNullOrEmpty(year))
            {
                blogs = _blogRepository.Get().ToList();
            }
            else
            {
                blogs = _blogRepository.Get().ToList();
            }
            return PartialView("_ArchivePartial", blogs);
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
            if (ModelState.IsValid)
            {
                model.DateAdded = DateTime.Now;
                model.UserId = ((CustomPrincipal)HttpContext.User).UserId;
                _blogRepository.Add(model);
                return RedirectToAction("Archive", "Blog", new { area = "Blogs" });
            }
            return View();
        }

        public ActionResult ViewBlogPost()
        {
            return View();
        }
    }
}
