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
using Siska.Data.Dao;
using Siska.Data.Model.Pos;
using Siska.Service;

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
            validUser = new User();

            validUser.UserName = "andi";
            validUser.Password = "andi";
            validUser.RecordStatus = true;
            validUser.UserId = 2;
            validUser.Roles.Add(new Role() {
                RoleName = "admin",
                RoleId = 1
            });
        }

        public Mock<IUserDao> SetupUserDaoMocking()
        {
            var mock = new Mock<IUserDao>();

            List<CriteriaParam> param = new List<CriteriaParam>();

            param.Add(new CriteriaParam() { FieldName = ServiceConstants.USERNAME, Operator = Operators.Equals, Value = "andi" });
            param.Add(new CriteriaParam() { FieldName = ServiceConstants.PASSWORD, Operator = Operators.Equals, Value = "andi" });
            param.Add(new CriteriaParam() { FieldName = ServiceConstants.RECORDSTATUS, Operator = Operators.Equals, Value = true });

            List<User> returnResult = new List<User>();
            returnResult.Add(validUser);

            mock.Setup(foo => foo.GetByCriteria(param)).Returns<List<User>>(x => x = returnResult);

            return mock;
        }

        public Mock<IUserSessionDao> SetupUserSessionDaoMock()
        {
            var mock = new Mock<IUserSessionDao>();

            return mock;
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
