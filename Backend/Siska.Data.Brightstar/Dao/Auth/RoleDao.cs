namespace Siska.Data.Dao.Auth
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using Siska.Data.Dao.Auth;
    using Siska.Data.Model.Auth;
    using BrightstarDB.EntityFramework;
    using System.Linq.Expressions;

    public class RoleDao : BaseDao, IRoleDao
    {
        ISiskaDB db;

        IEntitySet<IRole> roles;

        public IEntitySet<IRole> Roles
        {
            get { return roles; }
        }

        public RoleDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
            roles = db.BsContext.Roles;
		}

        public IRole CreateNew()
        {
            return db.BsContext.Roles.Create();
        }

        public virtual IRole Get(string id)
        {
            IRole role;

            role = (from a in db.BsContext.Roles
                            where a.Id.Equals(id)
                            select a).FirstOrDefault();

            return role;
        }

        public virtual IList<IRole> GetAll()
        {
            IList<IRole> roles;

            roles = (from a in db.BsContext.Roles
                        select a).ToList();

            return roles;
        }

        public virtual IList<IRole> GetByCriteria(Expression expression)
        {
            IList<IRole> result;

            result = db.BsContext.Roles.Provider.CreateQuery<IRole>(expression).ToList();

            return result;
        }

        public virtual void SaveChanges()
        {
            db.BsContext.SaveChanges();
        }

        public virtual IList<IRole> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            IList<IRole> result = db.BsContext.Roles.Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Roles.Count();            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());            

            return result;
        }

        public virtual IList<IRole> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, Expression expression)
        {
            numberOfPages = 0;

            IList<IRole> result = db.BsContext.Roles.Provider.CreateQuery<IRole>(expression).Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Roles.Count();

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return result;
        }

        public virtual void Delete(IRole entity)
        {
            db.BsContext.DeleteObject(entity);
        }
    }
}
