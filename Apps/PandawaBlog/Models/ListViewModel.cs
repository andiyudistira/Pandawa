namespace PandawaBlog.Models
{
    using Siska.Data.Model.Blog;
    using System.Collections.Generic;

    public class ListViewModel
    {
        public IList<IPost> Posts { get; set; }
        public int TotalPosts { get; set; }
    }
}