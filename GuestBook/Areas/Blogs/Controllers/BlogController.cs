using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.Contracts;
using GuestBook.Areas.Blogs.Models;
using GuestBook.Controllers;
using MongoLibrary;
using DTO;
using GuestBook.Security;
namespace GuestBook.Areas.Blogs.Controllers
{
    public class BlogController : BaseController
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
                model.ShowYear = true;
            }
            else if (!string.IsNullOrEmpty(month))
            {
                model.ShowMonth = true;
            }
            else if (dt.HasValue)
            {
                model.ShowDay = true;
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
