namespace Siska.Data.Dao.Pos
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Collections;
    using System.Collections.Generic;
    using Siska.Core;
    using Siska.Data.Dao;
    using Siska.Data.Model.Pos;
    using NDatabase;
    using NDatabase.Api.Query;

    public class RoleDao : BaseDao, IRoleDao
    {
        ISiskaDB db;

        public RoleDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
		}

        public Role Get(int id)
        {
            Role role;

            using (var odb = OdbFactory.OpenLast())
            {
                role = (from a in odb.AsQueryable<Role>()
                             where a.RoleId.Equals(id)
                             select a).FirstOrDefault();
            }

            return role;
        }

        public IList<Role> GetAll()
        {
            IList<Role> roles;

            using (var odb = OdbFactory.OpenLast())
            {
                roles = (from a in odb.AsQueryable<Role>()
                         select a).ToList();                
            }

            return roles;
        }

        public IList<Role> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            IList<Role> result;

            using (var odb = OdbFactory.OpenLast())
            {
                IQuery query = odb.Query<Role>();

                query = NDBSodaQueryCriteria(criteriaParam, query);

                result = query.Execute<Role>().ToList();
            }

            return result;
        }

        public string Add(Role entity)
        {
            string result;

            using (var odb = OdbFactory.OpenLast())
            {
                result = odb.Store<Role>(entity).ToString();
            }

            return result;
        }

        public void Update(Role entity)
        {
            //getSession().SaveOrUpdate(entity);
        }

        public IList<Role> GetAll(int page, int maxRow, out int numberOfPages)
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

        public IList<Role> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param)
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

        public void Delete(Role entity)
        {
            //getSession().Delete(entity);
        }
    }
}
