namespace Siska.Data.Dao.Pos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Siska.Core;
    using Siska.Data.BDao;
    using Siska.Data.BModel.Pos;

    public class UserSessionDao : IUserSessionDao
    {
        private ISiskaDB db;

        public UserSessionDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
		}

        public IUserSession Get(string id)
        {
            return null;
        }

        public IList<IUserSession> GetAll()
        {
            return null;
        }

        public IList<IUserSession> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            return null;
        }

        public string Add(IUserSession entity)
        {
            return string.Empty;
        }

        public void Update(IUserSession entity)
        {
            //getSession().SaveOrUpdate(entity);
        }

        public IList<IUserSession> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            //ICriteria criteria = getSession().CreateCriteria<UserSession>();
            //criteria.SetFirstResult(page * maxRow);
            //criteria.SetMaxResults(maxRow);

            //int totalRow = getSession().CreateCriteria<UserSession>().List().Count;            

            //double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            //numberOfPages = int.Parse(totalPages.ToString());

            //return criteria.List<UserSession>();       

            return null;
        }

        public IList<IUserSession> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param)
        {
            numberOfPages = 0;

            //ICriteria criteria = CreateCriteriaOnly<UserSession>(Param);
            //criteria.SetFirstResult(page * maxRow);
            //criteria.SetMaxResults(maxRow);

            //int totalRow = getSession().CreateCriteria<UserSession>().List().Count;

            //double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            //numberOfPages = int.Parse(totalPages.ToString());

            //return criteria.List<UserSession>(); 
            return null;
        }

        public void Delete(IUserSession entity)
        {
            //getSession().Delete(entity);
        }
    }
}
