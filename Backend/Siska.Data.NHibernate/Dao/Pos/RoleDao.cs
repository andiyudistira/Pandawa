using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Transactions;
using NHibernate;
using NHibernate.Transform;
using Siska.Core;
using Siska.Data.Dao;
using Siska.Data.Model.Pos;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Siska.Data.NHibernate.Dao.Pos
{
    public class RoleDao : HibernateDao, IRoleDao
    {
        public RoleDao(Func<ISession> getSession)
            : base(getSession)
		{
		}

        [Transaction]
        public Role Get(int id)
        {
            return getSession().Get<Role>(id);
        }
        
        [Transaction]
        public IList<Role> GetAll()
        {
            IList<Role> result;

            using (var s = getSession())
            {
                ICriteria criteria = getSession().CreateCriteria<Role>()
                                    .SetFetchMode("Users", FetchMode.Eager)
                                    .SetResultTransformer(new DistinctRootEntityResultTransformer());

                result = criteria.List<Role>();
            }

            return result;
        }

        [Transaction]
        public IList<Role> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            ICriteria criteria = CreateCriteriaOnly<Role>(criteriaParam)
                                    .SetFetchMode("Users", FetchMode.Eager)
                                    .SetResultTransformer(new DistinctRootEntityResultTransformer());

            return criteria.List<Role>();
        }

        [Transaction]
        public string Add(Role entity)
        {
            return (string)getSession().Save(entity).ToString();
        }

        [Transaction]
        public void Update(Role entity)
        {
            getSession().SaveOrUpdate(entity);
        }

        [Transaction]
        public IList<Role> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            ICriteria criteria = getSession().CreateCriteria<Role>()
                                    .SetFetchMode("Users", FetchMode.Eager)
                                    .SetResultTransformer(new DistinctRootEntityResultTransformer());

            criteria.SetFirstResult(page * maxRow);
            criteria.SetMaxResults(maxRow);

            int totalRow = getSession().CreateCriteria<Role>().List().Count;            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return criteria.List<Role>();            
        }

        [Transaction]
        public IList<Role> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param)
        {
            numberOfPages = 0;

            ICriteria criteria = CreateCriteriaOnly<Role>(Param)
                                    .SetFetchMode("Users", FetchMode.Eager)
                                    .SetResultTransformer(new DistinctRootEntityResultTransformer());

            criteria.SetFirstResult(page * maxRow);
            criteria.SetMaxResults(maxRow);

            int totalRow = getSession().CreateCriteria<Role>().List().Count;

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return criteria.List<Role>(); 
        }

        [Transaction]
        public void Delete(Role entity)
        {
            getSession().Delete(entity);
        }
    }
}
