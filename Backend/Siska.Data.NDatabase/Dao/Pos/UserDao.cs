namespace Siska.Data.Dao.Pos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Siska.Core;
    using Siska.Data.Dao;
    using Siska.Data.Model.Pos;

    public class UserDao : IUserDao
    {
        private ISiskaDB db;

        public UserDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
		}

        public User Get(int id)
        {
            return null;
        }
        
        public IList<User> GetAll()
        {
            return null;
        }

        public IList<User> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            return null;
        }

        public string Add(User entity)
        {
            return string.Empty;
        }
        
        public void Update(User entity)
        {
            
        }

        public IList<User> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            //ICriteria criteria = getSession().CreateCriteria<User>()
            //                        .SetFetchMode("Roles", FetchMode.Eager)
            //                        .SetFetchMode("UserSessions", FetchMode.Eager)
            //                        .SetResultTransformer(new DistinctRootEntityResultTransformer());

            //criteria.SetFirstResult(page * maxRow);
            //criteria.SetMaxResults(maxRow);

            //int totalRow = getSession().CreateCriteria<User>().List().Count;            

            //double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            //numberOfPages = int.Parse(totalPages.ToString());

            //return criteria.List<User>();            

            return null;
        }

        public IList<User> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param)
        {
            numberOfPages = 0;

            //ICriteria criteria = CreateCriteriaOnly<User>(Param)
            //                        .SetFetchMode("Roles", FetchMode.Eager)
            //                        .SetFetchMode("UserSessions", FetchMode.Eager)
            //                        .SetResultTransformer(new DistinctRootEntityResultTransformer());

            //criteria.SetFirstResult(page * maxRow);
            //criteria.SetMaxResults(maxRow);

            //int totalRow = getSession().CreateCriteria<User>().List().Count;

            //double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            //numberOfPages = int.Parse(totalPages.ToString());

            //return criteria.List<User>(); 
            return null;
        }

        public void Delete(User entity)
        {
            //getSession().Delete(entity);
        }
    }
}
