using Castle.Transactions;
using NHibernate;
using Siska.Core;
using Siska.Data.Dao;
using Siska.Data.Model.Acc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siska.Data.NHibernate.Dao.Acc
{
    public class CategoryDao : HibernateDao, ICategoryDao
    {
        public CategoryDao(Func<ISession> getSession)
            : base(getSession)
		{
		}

        [Transaction]
        public AccCategory Get(int id)
        {
            return getSession().Get<AccCategory>(id);
        }

        [Transaction]
        public IList<AccCategory> GetAll()
        {
            return GetAll<AccCategory>();
        }

        [Transaction]
        public IList<AccCategory> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            return CreateCriteria<AccCategory>(criteriaParam); 
        }

        [Transaction]
        public string Add(AccCategory entity)
        {
            return (string)getSession().Save(entity);
        }

        [Transaction]
        public void Update(AccCategory entity)
        {
            getSession().SaveOrUpdate(entity);
        }

        [Transaction]
        public IList<AccCategory> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            ICriteria criteria = getSession().CreateCriteria<AccCategory>();
            criteria.SetFirstResult(page * maxRow);
            criteria.SetMaxResults(maxRow);

            int totalRow = getSession().CreateCriteria<AccCategory>().List().Count;            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return criteria.List<AccCategory>();            
        }

        [Transaction]
        public void Delete(AccCategory entity)
        {
            getSession().Delete(entity);
        }


        public IList<AccCategory> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param)
        {
            throw new NotImplementedException();
        }
    }
}
