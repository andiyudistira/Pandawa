using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Transactions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Siska.Core;
using Siska.Data.Dao;
using Siska.Data.Model.Pos;

namespace Siska.Data.NHibernate.Dao.Pos
{
    public class UserDao : HibernateDao, IUserDao
    {
        public UserDao(Func<ISession> getSession)
            : base(getSession)
		{
		}

        [Transaction]
        public User Get(int id)
        {
            return getSession().Get<User>(id);
        }
        
        [Transaction]
        public IList<User> GetAll()
        {
            ICriteria criteria = getSession().CreateCriteria<User>()
                                    .SetFetchMode("Roles", FetchMode.Eager)
                                    .SetFetchMode("UserSessions", FetchMode.Eager)
                                    .SetResultTransformer(new DistinctRootEntityResultTransformer());

            return criteria.List<User>();
        }

        [Transaction]
        public IList<User> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            ICriteria criteria = CreateCriteriaOnly<User>(criteriaParam)
                                    .SetFetchMode("Roles", FetchMode.Eager)
                                    .SetFetchMode("UserSessions", FetchMode.Eager)
                                    .SetResultTransformer(new DistinctRootEntityResultTransformer());

            return criteria.List<User>();
        }

        [Transaction]
        public string Add(User entity)
        {
            return (string)getSession().Save(entity).ToString();
        }

        [Transaction]
        public void Update(User entity)
        {
            getSession().SaveOrUpdate(entity);
        }

        [Transaction]
        public IList<User> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            ICriteria criteria = getSession().CreateCriteria<User>()
                                    .SetFetchMode("Roles", FetchMode.Eager)
                                    .SetFetchMode("UserSessions", FetchMode.Eager)
                                    .SetResultTransformer(new DistinctRootEntityResultTransformer());

            criteria.SetFirstResult(page * maxRow);
            criteria.SetMaxResults(maxRow);

            int totalRow = getSession().CreateCriteria<User>().List().Count;            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return criteria.List<User>();            
        }

        [Transaction]
        public IList<User> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param)
        {
            numberOfPages = 0;

            ICriteria criteria = CreateCriteriaOnly<User>(Param)
                                    .SetFetchMode("Roles", FetchMode.Eager)
                                    .SetFetchMode("UserSessions", FetchMode.Eager)
                                    .SetResultTransformer(new DistinctRootEntityResultTransformer());

            criteria.SetFirstResult(page * maxRow);
            criteria.SetMaxResults(maxRow);

            int totalRow = getSession().CreateCriteria<User>().List().Count;

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return criteria.List<User>(); 
        }

        [Transaction]
        public void Delete(User entity)
        {
            getSession().Delete(entity);
        }
    }
}
