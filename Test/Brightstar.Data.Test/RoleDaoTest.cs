using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siska.Data;
using Siska.Data.BDao;
using Siska.Data.BModel.Pos;
using System.Linq;
using Brightstar.Data.Test.BrightstarHelper;
using Siska.Core;

namespace Brightstar.Data.Test
{
    [TestClass]
    public class RoleDaoTest : BrightstarUnitTest
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

        [TestInitialize]
        public void SetupDaoSession()
        {
            RoleDao = container.Resolve<IRoleDao>();
            UserDao = container.Resolve<IUserDao>();
        }

        [TestMethod]
        public void InsertRoleTest()
        {
            IUser usr = UserDao.CreateNew();
            IRole rl = RoleDao.CreateNew();

            usr.UserName = "Deni";
            usr.Password = "Deni";
            usr.UserId = 1;            
            usr.Roles = new List<IRole>();
            usr.Roles.Add(rl);
            usr.RecordStatus = true;

            rl.InsertBy = usr;
            rl.InsertDate = DateTime.Today;
            rl.UpdateDate = DateTime.Today;
            rl.RecordStatus = true;
            rl.RoleId = 2;
            rl.RoleName = "Operator";
            rl.Users = new Collection<IUser>();
            rl.Users.Add(usr);            

            string a = RoleDao.Add(rl);

            var test = RoleDao.GetAll();
        }

        [TestMethod]
        public void GetByCriteriaTest()
        {
            List<CriteriaParam> param = new List<CriteriaParam>();

            param.Add(new CriteriaParam() { FieldName = "RoleName", Operator = Operators.Equals, Value = "Admin" });
            param.Add(new CriteriaParam() { FieldName = "RoleId", Operator = Operators.Equals, Value = 1 });
            
            IList<IRole> testCriteria = RoleDao.GetByCriteria(param);

        }
    }
}
