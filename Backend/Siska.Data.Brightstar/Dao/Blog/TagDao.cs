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

    public class TagDao : ITagDao
    {
        ISiskaDB db;

        IEntitySet<ITag> tags;

        public IEntitySet<ITag> Tags
        {
            get { return tags; }
        }

        public TagDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
            tags = db.BsContext.Tags;
		}

        public ITag CreateNew()
        {
            return db.BsContext.Tags.Create();
        }

        public virtual ITag Get(string id)
        {
            ITag result;

            result = (from a in db.BsContext.Tags
                            where a.Id.Equals(id)
                            select a).FirstOrDefault();

            return result;
        }

        public virtual IList<ITag> GetAll()
        {
            IList<ITag> result;

            result = (from a in db.BsContext.Tags
                        select a).ToList();

            return result;
        }

        public virtual IList<ITag> GetByCriteria(Expression expression)
        {
            IList<ITag> result;

            result = db.BsContext.Tags.Provider.CreateQuery<ITag>(expression).ToList();

            return result;
        }

        public virtual void SaveChanges()
        {
            db.BsContext.SaveChanges();
        }

        public virtual IList<ITag> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            IList<ITag> result = db.BsContext.Tags.Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Tags.Count();            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());            

            return result;
        }

        public virtual IList<ITag> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, Expression expression)
        {
            numberOfPages = 0;

            IList<ITag> result = db.BsContext.Tags.Provider.CreateQuery<ITag>(expression).Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Tags.Count();

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return result;
        }

        public virtual void Delete(ITag entity)
        {
            db.BsContext.DeleteObject(entity);
        }

        public virtual int TotalRecords()
        {
            return db.BsContext.Tags.Count();
        }
    }
}
