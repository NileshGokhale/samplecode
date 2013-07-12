#region imports
using System.Collections.Generic;
using DataAccessObjects;

#endregion

namespace GuestBook.Areas.Blogs.Models
{
    /// <summary>
    /// View model to show blogs archives
    /// </summary>
    public class ArchiveViewModel
    {
        /// <summary>
        /// Gets or sets the blogs.
        /// </summary>
        /// <value>
        /// The blogs.
        /// </value>
        public List<Blog> Blogs { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show year].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show year]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowYear { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show month].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show month]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowMonth { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show day].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show day]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowDay { get; set; }
    }
}