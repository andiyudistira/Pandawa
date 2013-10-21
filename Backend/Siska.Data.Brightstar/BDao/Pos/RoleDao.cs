namespace Siska.Data.Dao.Pos
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using Siska.Data.BDao;
    using Siska.Data.BModel.Pos;
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

        public IRole Get(string id)
        {
            IRole role;

            role = (from a in db.BsContext.Roles
                            where a.Id.Equals(id)
                            select a).FirstOrDefault();

            return role;
        }

        public IList<IRole> GetAll()
        {
            IList<IRole> roles;

            roles = (from a in db.BsContext.Roles
                        select a).ToList();

            return roles;
        }

        public IList<IRole> GetByCriteria(Expression expression)
        {
            IList<IRole> result;

            result = db.BsContext.Roles.Provider.CreateQuery<IRole>(expression).ToList();

            return result;
        }

        public string Add(IRole entity)
        {
            db.BsContext.Roles.Add(entity);
            db.BsContext.SaveChanges();
            
            return entity.Id;
        }

        public void Update(IRole entity)
        {
            db.BsContext.Roles.Add(entity);
            db.BsContext.SaveChanges();
        }

        public IList<IRole> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            IList<IRole> result = db.BsContext.Roles.Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Roles.Count();            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());            

            return result;
        }

        public IList<IRole> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, Expression expression)
        {
            numberOfPages = 0;

            IList<IRole> result = db.BsContext.Roles.Provider.CreateQuery<IRole>(expression).Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Roles.Count();

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return result;
        }

        public void Delete(IRole entity)
        {
            db.BsContext.DeleteObject(entity);
        }
    }
}
