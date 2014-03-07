namespace Siska.Service.Blog
{
    using Siska.Core;
    using Siska.Data.Dao.Blog;
    using Siska.Data.Model.Blog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class BlogService : IBlogService
    {
        IPostDao postDao; 
        ICategoryDao categoryDao;
        ITagDao tagDao;

        public BlogService(IPostDao postDao, ICategoryDao categoryDao, ITagDao tagDao)
        {
            this.postDao = postDao;
            this.categoryDao = categoryDao;
            this.tagDao = tagDao;
        }

        public ServiceResponse LatestPost()
        {
            ServiceResponse response = new ServiceResponse(false);

            IList<IPost> result = new List<IPost>();

            Expression expr = (new List<IPost>()).Where(x =>
                x.PostedOn >= DateTime.Today.AddDays(-10) &&
                x.PostedOn >= DateTime.Today &&
                x.Published).AsQueryable().Expression;

            result = postDao.GetByCriteria(expr);

            response.Data = result;
            response.IsSuccess = true;

            return response;
        }

        public int TotalPost()
        {
            return postDao.TotalRecords();
        }
    }
}
