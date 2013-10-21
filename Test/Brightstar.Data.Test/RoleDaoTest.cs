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
using System.Linq.Expressions;

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

            PrepareData();
        }

        #region Prepare Data
        public void PrepareData()
        {
            IUser usrDeni = UserDao.CreateNew();
            IUser usrAndi = UserDao.CreateNew();

            IRole rlOperator = RoleDao.CreateNew();
            IRole rlAdmin = RoleDao.CreateNew();
            IRole rlGuest = RoleDao.CreateNew();
            IRole rlSupervisor = RoleDao.CreateNew();
            IRole rlStaff = RoleDao.CreateNew();

            usrDeni.UserName = "Deni";
            usrDeni.Password = "Deni";
            usrDeni.UserId = 1;            
            usrDeni.Roles = new List<IRole>();
            usrDeni.Roles.Add(rlOperator);
            usrDeni.RecordStatus = true;

            usrAndi.UserName = "Andi";
            usrAndi.Password = "Andi";
            usrAndi.UserId = 2;
            usrAndi.Roles = new List<IRole>();
            usrAndi.Roles.Add(rlOperator);
            usrAndi.Roles.Add(rlAdmin);
            usrAndi.Roles.Add(rlGuest);
            usrAndi.Roles.Add(rlSupervisor);
            usrAndi.Roles.Add(rlStaff);
            usrAndi.RecordStatus = true;

            rlOperator.InsertBy = usrDeni;
            rlOperator.InsertDate = DateTime.Today;
            rlOperator.UpdateDate = DateTime.Today;
            rlOperator.RecordStatus = true;
            rlOperator.RoleId = 1;
            rlOperator.RoleName = "Operator";
            rlOperator.Users = new Collection<IUser>();
            rlOperator.Users.Add(usrDeni);
            rlOperator.Users.Add(usrAndi);

            rlAdmin.InsertBy = usrDeni;
            rlAdmin.InsertDate = DateTime.Today;
            rlAdmin.UpdateDate = DateTime.Today;
            rlAdmin.RecordStatus = true;
            rlAdmin.RoleId = 2;
            rlAdmin.RoleName = "Admin";
            rlAdmin.Users = new Collection<IUser>();
            rlAdmin.Users.Add(usrAndi);

            rlGuest.InsertBy = usrDeni;
            rlGuest.InsertDate = DateTime.Today;
            rlGuest.UpdateDate = DateTime.Today;
            rlGuest.RecordStatus = true;
            rlGuest.RoleId = 3;
            rlGuest.RoleName = "Guest";
            rlGuest.Users = new Collection<IUser>();
            rlGuest.Users.Add(usrAndi);

            rlSupervisor.InsertBy = usrDeni;
            rlSupervisor.InsertDate = DateTime.Today;
            rlSupervisor.UpdateDate = DateTime.Today;
            rlSupervisor.RecordStatus = true;
            rlSupervisor.RoleId = 4;
            rlSupervisor.RoleName = "Supervisor";
            rlSupervisor.Users = new Collection<IUser>();
            rlSupervisor.Users.Add(usrAndi);

            rlStaff.InsertBy = usrDeni;
            rlStaff.InsertDate = DateTime.Today;
            rlStaff.UpdateDate = DateTime.Today;
            rlStaff.RecordStatus = true;
            rlStaff.RoleId = 5;
            rlStaff.RoleName = "Staff";
            rlStaff.Users = new Collection<IUser>();
            rlStaff.Users.Add(usrAndi);

            RoleDao.Add(rlOperator);
            RoleDao.Add(rlAdmin);
            RoleDao.Add(rlGuest);
            RoleDao.Add(rlSupervisor);
            RoleDao.Add(rlStaff);
        }
        #endregion

        [TestMethod]
        public void GetByCriteriaTest()
        {
            string roleName = "Operator";
            Expression expr = RoleDao.Roles.Where(x => x.RoleName.Equals(roleName)).Expression;

            IList<IRole> testCriteria = RoleDao.GetByCriteria(expr);

            Assert.IsNotNull(testCriteria);
            Assert.IsTrue(testCriteria.Count > 0);
            Assert.IsTrue(testCriteria[0].RoleName.Equals(roleName));
        }
    }
}
