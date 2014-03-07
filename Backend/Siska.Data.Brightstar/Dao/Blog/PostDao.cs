namespace Siska.Data.Dao.Blog
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using BrightstarDB.EntityFramework;
    using Siska.Core;
    using Siska.Data.Dao.Blog;
    using Siska.Data.Model.Blog;
    using System.Linq.Expressions;

    public class PostDao : IPostDao
    {
        ISiskaDB db;

        IEntitySet<IPost> posts;

        public IEntitySet<IPost> Posts
        {
            get { return posts; }
        }

        public PostDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
            posts = db.BsContext.Posts;
		}

        public IPost CreateNew()
        {
            return db.BsContext.Posts.Create();
        }

        public virtual IPost Get(string id)
        {
            IPost result;

            result = (from a in db.BsContext.Posts
                            where a.Id.Equals(id)
                            select a).FirstOrDefault();

            return result;
        }

        public virtual IList<IPost> GetAll()
        {
            IList<IPost> result;

            result = (from a in db.BsContext.Posts
                        select a).ToList();

            return result;
        }

        public virtual IList<IPost> GetByCriteria(Expression expression)
        {
            IList<IPost> result;

            result = db.BsContext.Posts.Provider.CreateQuery<IPost>(expression).ToList();

            return result;
        }

        public virtual void SaveChanges()
        {
            db.BsContext.SaveChanges();
        }

        public virtual IList<IPost> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            IList<IPost> result = db.BsContext.Posts.Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Posts.Count();            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());            

            return result;
        }

        public virtual IList<IPost> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, Expression expression)
        {
            numberOfPages = 0;

            IList<IPost> result = db.BsContext.Users.Provider.CreateQuery<IPost>(expression).Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Posts.Count();

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return result;
        }

        public virtual void Delete(IPost entity)
        {
            db.BsContext.DeleteObject(entity);
        }

        public virtual int TotalRecords()
        {
            return db.BsContext.Posts.Count();
        }
    }
}
