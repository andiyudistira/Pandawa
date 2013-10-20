namespace Siska.Data.Dao.Pos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Siska.Core;
    using Siska.Data.BDao;
    using Siska.Data.BModel.Pos;

    public class UserDao : IUserDao
    {
        private ISiskaDB db;

        public UserDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
		}

        public IUser CreateNew()
        {
            return db.BsContext.Users.Create();
        }

        public IUser Get(string id)
        {
            return null;
        }
        
        public IList<IUser> GetAll()
        {
            return null;
        }

        public IList<IUser> GetByCriteria(List<CriteriaParam> criteriaParam)
        {
            return null;
        }

        public string Add(IUser entity)
        {
            return string.Empty;
        }
        
        public void Update(IUser entity)
        {
            
        }

        public IList<IUser> GetAll(int page, int maxRow, out int numberOfPages)
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

        public IList<IUser> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param)
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

        public void Delete(IUser entity)
        {
            //getSession().Delete(entity);
        }
    }
}
