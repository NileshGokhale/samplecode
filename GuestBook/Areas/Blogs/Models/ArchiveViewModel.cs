#region imports
using System.Collections.Generic;
#endregion

namespace GuestBook.Areas.Blogs.Models
{
    public class ArchiveViewModel
    {
        public List<DTO.Blog> Blogs { get; set; }
        public bool ShowYear { get; set; }
        public bool ShowMonth { get; set; }
        public bool ShowDay { get; set; }
    }
}