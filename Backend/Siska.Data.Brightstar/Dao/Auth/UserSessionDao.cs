namespace Siska.Data.Dao.Auth
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using BrightstarDB.EntityFramework;
    using Siska.Data.Dao.Auth;
    using Siska.Data.Model.Auth;
    using System.Linq.Expressions;

    public class UserSessionDao : IUserSessionDao
    {
        ISiskaDB db;

        IEntitySet<IUserSession> usersessions;

        public IEntitySet<IUserSession> UserSessions
        {
            get { return usersessions; }
        }

        public UserSessionDao(ISiskaDB siskaDB)
        {
            db = siskaDB;
            usersessions = db.BsContext.UserSessions;
        }

        public IUserSession CreateNew()
        {
            return db.BsContext.UserSessions.Create();
        }

        public virtual IUserSession Get(string id)
        {
            IUserSession result;

            result = (from a in db.BsContext.UserSessions
                      where a.Id.Equals(id)
                      select a).FirstOrDefault();

            return result;
        }

        public virtual IList<IUserSession> GetAll()
        {
            IList<IUserSession> result;

            result = (from a in db.BsContext.UserSessions
                      select a).ToList();

            return result;
        }

        public virtual IList<IUserSession> GetByCriteria(Expression expression)
        {
            IList<IUserSession> result;

            result = db.BsContext.UserSessions.Provider.CreateQuery<IUserSession>(expression).ToList();

            return result;
        }

        public virtual void SaveChanges()
        {
            db.BsContext.SaveChanges();
        }

        public virtual IList<IUserSession> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            IList<IUserSession> result = db.BsContext.UserSessions.Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.UserSessions.Count();

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return result;
        }

        public virtual IList<IUserSession> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, Expression expression)
        {
            numberOfPages = 0;

            IList<IUserSession> result = db.BsContext.UserSessions.Provider.CreateQuery<IUserSession>(expression).Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.UserSessions.Count();

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return result;
        }

        public virtual void Delete(IUserSession entity)
        {
            db.BsContext.DeleteObject(entity);
        }

        public virtual int TotalRecords()
        {
            return db.BsContext.UserSessions.Count();
        }
    }
}
