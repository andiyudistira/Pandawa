using Castle.Transactions;
using NHibernate;
using Siska.Core;
using Siska.Data.Common.Acc;
using Siska.Data.Model.Acc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siska.Data.NHibernate.Dao.Acc
{
    public class AccountDao : HibernateDao, IAccountDao
    {
        public AccountDao(Func<ISession> getSession)
            : base(getSession)
		{
		}

        [Transaction]
        public AccAccount Get(int id)
        {
            return getSession().Get<AccAccount>(id);
        }

        [Transaction]
        public IList<AccAccount> GetAll()
        {
            return GetAll<AccAccount>();
        }

        [Transaction]
        public IList<AccAccount> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            return CreateCriteria<AccAccount>(criteriaParam); 
        }

        [Transaction]
        public string Add(AccAccount entity)
        {
            return (string)getSession().Save(entity);
        }

        [Transaction]
        public void Update(AccAccount entity)
        {
            getSession().SaveOrUpdate(entity);
        }

        [Transaction]
        public IList<AccAccount> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            ICriteria criteria = getSession().CreateCriteria<AccAccount>();
            criteria.SetFirstResult(page * maxRow);
            criteria.SetMaxResults(maxRow);

            int totalRow = getSession().CreateCriteria<AccAccount>().List().Count;            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return criteria.List<AccAccount>();            
        }

        [Transaction]
        public void Delete(AccAccount entity)
        {
            getSession().Delete(entity);
        }
    }
}
