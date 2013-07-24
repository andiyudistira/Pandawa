using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siska.Core;
using Siska.Data.Dao;
using Siska.Data.Model.Pos;
using Siska.Data.NHibernate.Dao.Pos;

namespace POS.Data.Test
{
    [TestClass]
    public class UserRoleDaoTest : NHUnitTest
    {
        private IUserDao UserDao
        {
            get;
            set;
        }

        private IRoleDao RoleDao
        {
            get;
            set;
        }

        private IUserSessionDao UserSessionDao
        {
            get;
            set;
        }
        
        [TestInitialize]
        public void SetupDaoSession()
        {
            UserDao = new UserDao(() => globalSession);
            RoleDao = new RoleDao(() => globalSession);
            UserSessionDao = new UserSessionDao(() => globalSession);
        }

        [TestMethod]
        public void SanityCheck()
        {
            Assert.IsNotNull(UserDao, "User dao is null");
        }

        [TestMethod]
        public void RecordFetchTest()
        {
            // Test Fetch all without parameter
            //var accountDt = UserDao.GetAll();

            // Test Fetch all with parameter
            List<CriteriaParam> param = new List<CriteriaParam>();                      

            //param.Add(new CriteriaParam() { FieldName = "UserName", Operator = Operators.Or, Value = "", Left = leftParam, Right = rightParam });
            param.Add(new CriteriaParam() { FieldName = "UserName", Operator = Operators.Equals, Value = "andi" });

            var userTest = UserDao.GetByCriteria(param);
        }

        [TestMethod]
        public void SaveEntityTest()
        {
            var userList = UserDao.GetAll();

            User userToBeSaved = new User();

            userToBeSaved.UserName = "daniel";
            userToBeSaved.Password = "daniel";
            userToBeSaved.RecordStatus = true;

            var roleList = RoleDao.GetAll();

            List<Role> roles = new List<Role>();

            roles.Add(roleList[0]);
            roles.Add(roleList[1]);

            userToBeSaved.Roles = roles;
            roleList[0].Users.Add(userToBeSaved);
            roleList[1].Users.Add(userToBeSaved);

            var result = UserDao.Add(userToBeSaved);
            RoleDao.Update(roleList[0]);
            RoleDao.Update(roleList[1]);
        }

        [TestMethod]
        public void AddUserSessionTest()
        {
            List<CriteriaParam> param = new List<CriteriaParam>();

            param.Add(new CriteriaParam() { FieldName = "UserName", Operator = Operators.Equals, Value = "andi" });
            param.Add(new CriteriaParam() { FieldName = "Password", Operator = Operators.Equals, Value = "andi" });
            param.Add(new CriteriaParam() { FieldName = "RecordStatus", Operator = Operators.Equals, Value = true });

            User user = UserDao.GetByCriteria(param).FirstOrDefault();

            UserSession userSession = new UserSession();

            userSession.LoginDate = DateTime.Now;
            userSession.LoginStatus = 1;
            userSession.SessionId = Guid.NewGuid();
            userSession.User = user;

            user.UserSessions.Add(userSession);

            UserSessionDao.Add(userSession);
        }
    }
}
