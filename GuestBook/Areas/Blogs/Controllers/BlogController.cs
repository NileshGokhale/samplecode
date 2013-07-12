using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccessObjects;
using GuestBook.Areas.Blogs.Models;
using GuestBook.Controllers;
using MongoLibrary;
using GuestBook.Security;
namespace GuestBook.Areas.Blogs.Controllers
{
    /// <summary>
    /// Controller handles blog posts
    /// </summary>
    public class BlogController : BaseController
    {
        readonly IGenericRepository<Blog> _blogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogController"/> class.
        /// </summary>
        /// <param name="blogRepository">The blog repository.</param>
        public BlogController(IGenericRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            var model = new ArchiveViewModel
                            {
                                Blogs = GetBlogs(null, null, null)
                            };
            return View(model);
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
            model.Blogs = GetBlogs(year, month, dt);
            if (!string.IsNullOrEmpty(year))
            {
                model.ShowMonth = true;
            }
            else if (!string.IsNullOrEmpty(month))
            {
                model.ShowDay = true;
            }
            else if (dt.HasValue)
            {
                model.ShowDay = true;
            }else
            {
                model.ShowYear = true;
            }
            return PartialView("_ArchivePartial", model);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Views the blog post.
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewBlogPost()
        {
            return View();
        }

        /// <summary>
        /// Gets the blogs.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns></returns>
        private List<Blog> GetBlogs(string year, string month, int? day)
        {
            List<Blog> blogs;
            if (!string.IsNullOrEmpty(year))
            {
                var compareYearFrom = new DateTime(Convert.ToInt32(year), 1, 1);
                var compareYearTo = new DateTime(Convert.ToInt32(year), 12, 31);
                blogs = _blogRepository.Get(x => x.DateAdded >= compareYearFrom && x.DateAdded <= compareYearTo).ToList();
            }
            else if (!string.IsNullOrEmpty(month))
            {
                var compareMonth = Convert.ToInt32(month);
                blogs = _blogRepository.Get(x => x.DateAdded.Month == compareMonth).ToList();
            }
            else if (day.HasValue)
            {
                blogs = _blogRepository.Get(x => x.Day == day.Value).ToList();
            }
            else
            {
                blogs = _blogRepository.Get().ToList();
            }
            return blogs;
        }
    }
}
