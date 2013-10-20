namespace Siska.Data.Dao.Pos
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Collections;
    using System.Collections.Generic;
    using Siska.Core;
    using Siska.Data.BDao;
    using Siska.Data.BModel.Pos;

    public class RoleDao : BaseDao, IRoleDao
    {
        ISiskaDB db;

        public RoleDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
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

        public IList<IRole> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            IList<IRole> result;

            //using (var odb = OdbFactory.OpenLast())
            //{
            //    IQuery query = odb.Query<Role>();

            //    query = NDBSodaQueryCriteria(criteriaParam, query);

            //    result = query.Execute<Role>().ToList();
            //}

            return null;
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

            //getSession().SaveOrUpdate(entity);
        }

        public IList<IRole> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            //ICriteria criteria = getSession().CreateCriteria<Role>()
            //                        .SetFetchMode("Users", FetchMode.Eager)
            //                        .SetResultTransformer(new DistinctRootEntityResultTransformer());

            //criteria.SetFirstResult(page * maxRow);
            //criteria.SetMaxResults(maxRow);

            //int totalRow = getSession().CreateCriteria<Role>().List().Count;            

            //double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            //numberOfPages = int.Parse(totalPages.ToString());

            //return criteria.List<Role>();   

            return null;
        }

        public IList<IRole> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param)
        {
            numberOfPages = 0;

            //ICriteria criteria = CreateCriteriaOnly<Role>(Param)
            //                        .SetFetchMode("Users", FetchMode.Eager)
            //                        .SetResultTransformer(new DistinctRootEntityResultTransformer());

            //criteria.SetFirstResult(page * maxRow);
            //criteria.SetMaxResults(maxRow);

            //int totalRow = getSession().CreateCriteria<Role>().List().Count;

            //double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            //numberOfPages = int.Parse(totalPages.ToString());

            //return criteria.List<Role>(); 

            return null;
        }

        public void Delete(IRole entity)
        {
            //getSession().Delete(entity);
        }
    }
}
