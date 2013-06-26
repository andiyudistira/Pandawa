using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siska.Core;
using Siska.Data.Dao;
using Siska.Data.Model.Pos;

namespace POS.Data.Test
{
    [TestClass]
    public class UserRoleDaoTest : NHUnitTest
    {
        private IUserDao UserDao
        {
            get { return Resolve<IUserDao>(); }
        }

        private IRoleDao RoleDao
        {
            get { return Resolve<IRoleDao>(); }
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
            var accountDt = UserDao.GetAll();

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
    }
}
