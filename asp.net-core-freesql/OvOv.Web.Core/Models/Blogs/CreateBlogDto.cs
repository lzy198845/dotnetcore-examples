using System.Collections.Generic;

namespace OvOv.Web.Core.Models.Blogs
{
    public class CreateBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public List<string> Tags { get; set; }

    }
}
