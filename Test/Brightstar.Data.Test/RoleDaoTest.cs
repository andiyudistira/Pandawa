using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siska.Data;
using Siska.Data.Dao.Auth;
using Siska.Data.Model.Auth;
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

            PrepareDataUser();
            PrepareDataRole();
        }

        #region Prepare Data
        private void PrepareDataUser()
        {
            IUser usrDeni = UserDao.CreateNew();
            IUser usrAndi = UserDao.CreateNew();

            usrDeni.UserName = "Deni";
            usrDeni.Password = "Deni";
            usrDeni.UserId = 1;
            usrDeni.Roles = new List<IRole>();
            usrDeni.RecordStatus = true;
            usrDeni.UserSessions = new List<IUserSession>();

            usrAndi.UserName = "Andi";
            usrAndi.Password = "Andi";
            usrAndi.UserId = 2;
            usrAndi.Roles = new List<IRole>();
            usrAndi.RecordStatus = true;
            usrAndi.UserSessions = new List<IUserSession>();

            UserDao.Users.Add(usrDeni);
            UserDao.Users.Add(usrAndi);
            UserDao.SaveChanges();
        }

        public void PrepareDataRole()
        {
            IList<IUser> userr = UserDao.GetAll();

            IRole rlOperator = RoleDao.CreateNew();
            IRole rlAdmin = RoleDao.CreateNew();
            IRole rlGuest = RoleDao.CreateNew();
            IRole rlSupervisor = RoleDao.CreateNew();
            IRole rlStaff = RoleDao.CreateNew();

            rlOperator.InsertBy = userr[1];
            rlOperator.InsertDate = DateTime.Today;
            rlOperator.UpdateDate = DateTime.Today;
            rlOperator.RecordStatus = true;
            rlOperator.RoleId = 1;
            rlOperator.RoleName = "Operator";
            rlOperator.Users = new Collection<IUser>();
            rlOperator.Users.Add(userr[0]);
            rlOperator.Users.Add(userr[1]);

            rlAdmin.InsertBy = userr[1];
            rlAdmin.InsertDate = DateTime.Today;
            rlAdmin.UpdateDate = DateTime.Today;
            rlAdmin.RecordStatus = true;
            rlAdmin.RoleId = 2;
            rlAdmin.RoleName = "Admin";
            rlAdmin.Users = new Collection<IUser>();
            rlAdmin.Users.Add(userr[1]);

            rlGuest.InsertBy = userr[1];
            rlGuest.InsertDate = DateTime.Today;
            rlGuest.UpdateDate = DateTime.Today;
            rlGuest.RecordStatus = true;
            rlGuest.RoleId = 3;
            rlGuest.RoleName = "Guest";
            rlGuest.Users = new Collection<IUser>();
            rlGuest.Users.Add(userr[1]);

            rlSupervisor.InsertBy = userr[1];
            rlSupervisor.InsertDate = DateTime.Today;
            rlSupervisor.UpdateDate = DateTime.Today;
            rlSupervisor.RecordStatus = true;
            rlSupervisor.RoleId = 4;
            rlSupervisor.RoleName = "Supervisor";
            rlSupervisor.Users = new Collection<IUser>();
            rlSupervisor.Users.Add(userr[1]);

            rlStaff.InsertBy = userr[1];
            rlStaff.InsertDate = DateTime.Today;
            rlStaff.UpdateDate = DateTime.Today;
            rlStaff.RecordStatus = true;
            rlStaff.RoleId = 5;
            rlStaff.RoleName = "Staff";
            rlStaff.Users = new Collection<IUser>();
            rlStaff.Users.Add(userr[1]);

            RoleDao.Roles.Add(rlOperator);
            RoleDao.Roles.Add(rlAdmin);
            RoleDao.Roles.Add(rlGuest);
            RoleDao.Roles.Add(rlSupervisor);
            RoleDao.Roles.Add(rlStaff);

            for (int i = 6; i < 9; i++)
            {
                IRole rlDummy = RoleDao.CreateNew();

                rlDummy.InsertBy = userr[1];
                rlDummy.InsertDate = DateTime.Today;
                rlDummy.UpdateDate = DateTime.Today;
                rlDummy.RecordStatus = true;
                rlDummy.RoleId = i;
                rlDummy.RoleName = "Dummy_" + i;
                rlDummy.Users = new Collection<IUser>();
                rlDummy.Users.Add(userr[0]);
                rlDummy.Users.Add(userr[1]);

                RoleDao.Roles.Add(rlDummy);
            }

            RoleDao.SaveChanges();
        }
        #endregion

        [TestMethod]
        public void GetByCriteriaTest()
        {
            string roleName = "Operator";

            var a = RoleDao.GetAll();
            var u = UserDao.GetAll();

            //Expression expr = RoleDao.Roles.Where(x => x.RoleName.Equals(roleName)).Expression;

            Expression expr = (new List<IRole>()).Where(x => x.RoleName.Equals(roleName)).AsQueryable().Expression;

            IList<IRole> testCriteria = RoleDao.GetByCriteria(expr);

            Assert.IsNotNull(testCriteria);
            Assert.IsTrue(testCriteria.Count > 0);
            Assert.IsTrue(testCriteria[0].RoleName.Equals(roleName));
            Assert.IsTrue(testCriteria[0].Users.Count() == 2);
        }

        [TestMethod]
        public void GetByCriteriaTestWithSelectMany()
        {
            string userName = "Andi";
            Expression expr2 = (//from a in RoleDao.Roles
                                from b in UserDao.Users
                                where b.UserName.Equals(userName)
                                select b).Expression;

            IList<IUser> testCriteriaWithJoin = UserDao.GetByCriteria(expr2);
            
            Assert.IsNotNull(testCriteriaWithJoin);

            IList<IRole> thisUserRole = testCriteriaWithJoin[0].Roles.ToList();

            //Assert.IsTrue(testCriteriaWithJoin.Count == 5);

            //foreach (IRole item in testCriteriaWithJoin)
            //{
            //    Assert.IsNotNull(item.Users);

            //    IUser user = item.Users.Where(x => x.UserName.Equals(userName)).FirstOrDefault();

            //    Assert.IsNotNull(user);
            //    Assert.IsTrue(user.UserName.Equals(userName));
            //}
        }
    }
}
