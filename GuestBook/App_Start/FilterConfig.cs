﻿using System.Web.Mvc;

namespace GuestBook.App_Start
{
    /// <summary>
    /// Filter config to register custom filters
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeFilter());
            filters.Add(new HandleErrorFilter());
        }
    }
}