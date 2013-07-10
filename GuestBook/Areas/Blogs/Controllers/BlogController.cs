using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.Contracts;
using GuestBook.Areas.Blogs.Models;
using MongoLibrary;
using DTO;
using GuestBook.Security;
namespace GuestBook.Areas.Blogs.Controllers
{
    public class BlogController : Controller
    {
        readonly IGenericRepository<Blog> _blogRepository;

        public BlogController(IGenericRepository<Blog> blogRepository)
        {
            this._blogRepository = blogRepository;
        }

        //
        // GET: /Blogs/Blog/

        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// Archives the specified year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public ActionResult Archive(string year, string month, int? dt)
        {
            var model = new ArchiveViewModel();
            List<Blog> blogs;
            if (!string.IsNullOrEmpty(year))
            {
                var compareYear = Convert.ToInt32(year);
                blogs = _blogRepository.Get(x => x.DateAdded.Year == compareYear).ToList();
                model.ShowYear = true;
                model.Blogs = blogs;
            }
            else if (!string.IsNullOrEmpty(month))
            {
                var compareMonth = Convert.ToInt32(month);
                blogs = _blogRepository.Get(x => x.DateAdded.Month == compareMonth).ToList();
                model.ShowMonth = true;
                model.Blogs = blogs;
            }
            else if (dt.HasValue)
            {
                blogs = _blogRepository.Get(x => x.Day == dt.Value).ToList();
            }
            else
            {
                blogs = _blogRepository.Get().ToList();
            }
            return PartialView("_ArchivePartial", model);
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
                return RedirectToAction("Index", "Blog", new { area = "Blogs" });
            }
            return View();
        }

        public ActionResult ViewBlogPost()
        {
            return View();
        }
    }
}
