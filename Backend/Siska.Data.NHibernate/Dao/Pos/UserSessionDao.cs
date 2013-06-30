using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Transactions;
using NHibernate;
using Siska.Core;
using Siska.Data.Dao;
using Siska.Data.Model.Pos;

namespace Siska.Data.NHibernate.Dao.Pos
{
    public class UserSessionDao : HibernateDao, IUserSessionDao
    {
        public UserSessionDao(Func<ISession> getSession)
            : base(getSession)
		{
		}

        [Transaction]
        public UserSession Get(Guid id)
        {
            return getSession().Get<UserSession>(id);
        }

        [Transaction]
        public IList<UserSession> GetAll()
        {
            ICriteria criteria = getSession().CreateCriteria<UserSession>();

            return criteria.List<UserSession>();
        }

        [Transaction]
        public IList<UserSession> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            return CreateCriteria<UserSession>(criteriaParam); 
        }

        [Transaction]
        public string Add(UserSession entity)
        {
            return (string)getSession().Save(entity).ToString();
        }

        [Transaction]
        public void Update(UserSession entity)
        {
            getSession().SaveOrUpdate(entity);
        }

        [Transaction]
        public IList<UserSession> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            ICriteria criteria = getSession().CreateCriteria<UserSession>();
            criteria.SetFirstResult(page * maxRow);
            criteria.SetMaxResults(maxRow);

            int totalRow = getSession().CreateCriteria<UserSession>().List().Count;            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return criteria.List<UserSession>();            
        }

        [Transaction]
        public IList<UserSession> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param)
        {
            numberOfPages = 0;

            ICriteria criteria = CreateCriteriaOnly<UserSession>(Param);
            criteria.SetFirstResult(page * maxRow);
            criteria.SetMaxResults(maxRow);

            int totalRow = getSession().CreateCriteria<UserSession>().List().Count;

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return criteria.List<UserSession>(); 
        }

        [Transaction]
        public void Delete(UserSession entity)
        {
            getSession().Delete(entity);
        }
    }
}
