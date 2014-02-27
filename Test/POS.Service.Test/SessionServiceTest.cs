using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Siska.Core;
using Siska.Data.Dao.Auth;
using Siska.Data.Model.Auth;
using Siska.Service;
using System.Linq.Expressions;
using BrightstarDB.EntityFramework;
using Siska.Data;
using Siska.Data.Dao;

namespace POS.Service.Test
{
    [TestClass]
    public class SessionServiceTest : ServiceBaseTest
    {
        User validUser { get; set; }
        UserSession validUserSession { get; set; }
        UserSession invalidUserSession { get; set; }

        private ISessionService SessionService
        {
            get { return Resolve<ISessionService>(); }
        }

        [TestInitialize]
        public void InitTest()
        {
            PrepareUser();

            PrepareUserSession();

            var userDaoMock = SetupUserDaoMocking();

            var userSessionMock = SetupUserSessionDaoMock();

            container.Register(Component
                        .For<IUserDao>()
                        .Instance(userDaoMock.Object));

            container.Register(Component
                        .For<IUserSessionDao>()
                        .Instance(userSessionMock.Object));

            AssemblyFilter filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "")); 

            container.Register(Classes.FromAssemblyInDirectory(filter)
                      .Where(c => c.IsClass &&
                                    (c.Name.Contains("Service")) && c.Assembly.GetName().Name == "Siska.Service")
                      .WithService.DefaultInterfaces()
                      .LifestyleSingleton()
                      .Configure(delegate(ComponentRegistration c) { var x = c.Interceptors(InterceptorReference.ForType<ServiceInterceptor>()).Anywhere; })
                    );
        }

        private void PrepareUserSession()
        {
            validUserSession = new UserSession();

            invalidUserSession = new UserSession();
        }

        private void PrepareUser()
        {
            validUser = SetupUserDaoMocking().Object.CreateNew() as User;
            
            validUser.UserName = "andi";
            validUser.Password = "andi";
            validUser.RecordStatus = true;
            validUser.UserId = 2;
            //validUser.Roles.Add(new Role() {
            //    RoleName = "admin",
            //    RoleId = 1
            //});
            //validUser.UserSessions = (new List<IUserSession>()).ToArray();
        
        }

        public Mock<UserDao> SetupUserDaoMocking()
        {
            ISiskaDB db = new SiskaDB();

            var mockUserDao = new Mock<UserDao>(new object[1] { db });
            mockUserDao.Object.CreateNew();

            List<CriteriaParam> param = new List<CriteriaParam>();

            param.Add(new CriteriaParam() { FieldName = ServiceConstants.USERNAME, Operator = Operators.Equals, Value = "andi" });
            param.Add(new CriteriaParam() { FieldName = ServiceConstants.PASSWORD, Operator = Operators.Equals, Value = "andi" });
            param.Add(new CriteriaParam() { FieldName = ServiceConstants.RECORDSTATUS, Operator = Operators.Equals, Value = true });

            Expression expr = (new List<IUser>()).Where(x =>
                    x.UserName.Equals("andi") &&
                    x.Password.Equals("andi") &&
                    x.RecordStatus).AsQueryable().Expression;

            List<IUser> returnResult = new List<IUser>();
            returnResult.Add(validUser);

            mockUserDao.Setup(foo => foo.GetByCriteria(It.IsAny<Expression>())).Returns(returnResult);

            return mockUserDao;
        }

        public Mock<UserSessionDao> SetupUserSessionDaoMock()
        {
            ISiskaDB db = new SiskaDB();

            var mockUserSessionDao = new Mock<UserSessionDao>(new object[1] { db });

            return mockUserSessionDao;
        }

        [TestMethod]
        public void StartSessionTest()
        {
            ServiceRequest req = new ServiceRequest();
            req.NoData = true;

            Hashtable serviceParams = new Hashtable();

            serviceParams.Add(ServiceConstants.USERNAME, "andi");
            serviceParams.Add(ServiceConstants.PASSWORD, "andi");
            serviceParams.Add(ServiceConstants.RECORDSTATUS, true);

            req.Parameters = serviceParams;

            ServiceResponse serviceResult = SessionService.StartSession(req);
        }
    }
}
