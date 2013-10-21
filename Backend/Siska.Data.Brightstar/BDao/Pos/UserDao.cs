namespace Siska.Data.Dao.Pos
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using BrightstarDB.EntityFramework;
    using Siska.Core;
    using Siska.Data.BDao;
    using Siska.Data.BModel.Pos;
    using System.Linq.Expressions;

    public class UserDao : IUserDao
    {
        ISiskaDB db;

        IEntitySet<IUser> users;

        public IEntitySet<IUser> Users
        {
            get { return users; }
        }

        public UserDao(ISiskaDB siskaDB)
		{
            db = siskaDB;
            users = db.BsContext.Users;
		}

        public IUser CreateNew()
        {
            return db.BsContext.Users.Create();
        }

        public IUser Get(string id)
        {
            IUser result;

            result = (from a in db.BsContext.Users
                            where a.Id.Equals(id)
                            select a).FirstOrDefault();

            return result;
        }

        public IList<IUser> GetAll()
        {
            IList<IUser> result;

            result = (from a in db.BsContext.Users
                        select a).ToList();

            return result;
        }

        public IList<IUser> GetByCriteria(Expression expression)
        {
            IList<IUser> result;

            result = db.BsContext.Users.Provider.CreateQuery<IUser>(expression).ToList();

            return result;
        }

        public string Add(IUser entity)
        {
            db.BsContext.Users.Add(entity);
            db.BsContext.SaveChanges();
            
            return entity.Id;
        }

        public void Update(IUser entity)
        {
            db.BsContext.Users.Add(entity);
            db.BsContext.SaveChanges();
        }

        public IList<IUser> GetAll(int page, int maxRow, out int numberOfPages)
        {
            numberOfPages = 0;

            IList<IUser> result = db.BsContext.Users.Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Users.Count();            

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());            

            return result;
        }

        public IList<IUser> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, Expression expression)
        {
            numberOfPages = 0;

            IList<IUser> result = db.BsContext.Users.Provider.CreateQuery<IUser>(expression).Skip(page * maxRow).Take(maxRow).ToList();

            int totalRow = db.BsContext.Users.Count();

            double totalPages = Math.Round(Convert.ToDouble(Convert.ToDouble(totalRow) / Convert.ToDouble(maxRow)), MidpointRounding.AwayFromZero);

            numberOfPages = int.Parse(totalPages.ToString());

            return result;
        }

        public void Delete(IUser entity)
        {
            db.BsContext.DeleteObject(entity);
        }
    }
}
