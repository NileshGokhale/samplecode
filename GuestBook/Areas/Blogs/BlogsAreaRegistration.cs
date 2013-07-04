using System.Web.Mvc;

namespace GuestBook.Areas.Blogs
{
    public class BlogsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Blogs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("BlogArchive",
                "Blogs/{controller}/{year}/{month}/{date}",
                new { action = "Archive" });

            //context.MapRoute("BlogArchiveYear",
            //   "Blogs/{controller}/{year}",
            //   new { action = "Archive" });

            //context.MapRoute("BlogArchiveYearMonth",
            //   "Blogs/{controller}/{year}/{month}",
            //   new { action = "Archive" });

            //context.MapRoute("BlogArchiveYearMonthDate",
            //   "Blogs/{controller}/{year}/{month}/{date}",
            //   new { action = "Archive" });

            context.MapRoute(
                "Blogs_default",
                "Blogs/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
