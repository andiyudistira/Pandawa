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

    public class CategoryDao : ICategoryDao
    {
        ISiskaDB db;

        IEntitySet<ICategory> categories;

        public IEntitySet<ICategory> Categories
        {
            get { return categories; }
        }

        public CategoryDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
            categories = db.BsContext.Categories;
		}

        public ICategory CreateNew()
        {
            return db.BsContext.Categories.Create();
        }

        public virtual ICategory Get(string id)
        {
            ICategory result;

            result = (from a in db.BsContext.Categories
                            where a.Id.Equals(id)
                            select a).FirstOrDefault();

            return result;
        }

        public virtual IList<ICategory> GetAll()
        {
            IList<ICategory> result;

            result = (from a in db.BsContext.Categories
                        select a).ToList();

            return result;
        }

        public virtual IList<ICategory> GetByCriteria(Expression expression)
        {
            IList<ICategory> result;

            result = db.BsContext.Users.Provider.CreateQuery<ICategory>(expression).ToList();

            return result;
        }

        public virtual void SaveChanges()
        {
            db.BsContext.SaveChanges();
        }

        public virtual IList<ICategory> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            IList<ICategory> result = db.BsContext.Categories.Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Categories.Count();            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());            

            return result;
        }

        public virtual IList<ICategory> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, Expression expression)
        {
            numberOfPages = 0;

            IList<ICategory> result = db.BsContext.Users.Provider.CreateQuery<ICategory>(expression).Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Categories.Count();

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return result;
        }

        public virtual void Delete(ICategory entity)
        {
            db.BsContext.DeleteObject(entity);
        }

        public virtual int TotalRecords()
        {
            return db.BsContext.Categories.Count();
        }
    }
}
