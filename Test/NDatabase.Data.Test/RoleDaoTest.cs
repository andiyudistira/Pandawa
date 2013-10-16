using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siska.Data;
using Siska.Data.Dao;
using Siska.Data.Model.Pos;
using System.Linq;
using NDatabase.Data.Test.NDatabaseHelper;
using Siska.Core;

namespace NDatabase.Data.Test
{
    [TestClass]
    public class RoleDaoTest : NDatabaseUnitTest
    {
        private IRoleDao RoleDao
        {
            get;
            set;
        }

        [TestInitialize]
        public void SetupDaoSession()
        {
            RoleDao = container.Resolve<IRoleDao>();
        }

        [TestMethod]
        public void InsertRoleTest()
        {
            User usr = new User();
            Role rl = new Role();

            usr.UserName = "Andi";
            usr.Password = "Andi";
            usr.UserId = 0;
            usr.Roles = new List<Role>();
            usr.Roles.Add(rl);
            usr.RecordStatus = true;

            rl.InsertBy = usr;
            rl.InsertDate = DateTime.Today;
            rl.RecordStatus = true;
            rl.RoleId = 1;
            rl.RoleName = "Admin";
            rl.Users = new Collection<User>();
            rl.Users.Add(usr);

            string a = RoleDao.Add(rl);

            var test = RoleDao.GetAll();
        }

        [TestMethod]
        public void GetByCriteriaTest()
        {
            List<CriteriaParam> param = new List<CriteriaParam>();

            param.Add(new CriteriaParam() { FieldName = "RoleName", Operator = Operators.Equals, Value = "Admin" });

            IList<Role> testCriteria = RoleDao.GetByCriteria(param);

        }
    }
}
