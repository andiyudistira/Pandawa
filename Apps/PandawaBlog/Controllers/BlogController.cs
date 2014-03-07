using PandawaBlog.Models;
using Siska.Data.Model.Blog;
using Siska.Service.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PandawaBlog.Controllers
{
    public class BlogController : Controller
    {
        IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public ViewResult Posts()
        {
            // TODO: read and return posts from repository
            IList<IPost> latestPost = blogService.LatestPost().Data as IList<IPost>;
            int totalPost = blogService.TotalPost();

            ListViewModel list = new ListViewModel()
            {
                Posts = latestPost,
                TotalPosts = totalPost
            };

            return View("List", latestPost);
        }
    }
}
