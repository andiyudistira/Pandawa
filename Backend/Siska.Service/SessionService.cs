using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Siska.Core;
using Siska.Data.Dao;
using Siska.Data.Dto.Pos;
using Siska.Data.Model.Pos;

namespace Siska.Service
{
    public class SessionService : ServiceBase, ISessionService
    {
        public IUserDao UserDao { get; set; }

        public IUserSessionDao UserSessionDao { get; set; }

        public ServiceResponse StartSession(ServiceRequest serviceParams)
        {
            ServiceResponse response = new ServiceResponse(false);

            List<CriteriaParam> param = new List<CriteriaParam>();

            string userName = serviceParams.Parameters[ServiceConstants.USERNAME].ToString();
            string password = serviceParams.Parameters[ServiceConstants.PASSWORD].ToString();

            param.Add(new CriteriaParam() { FieldName = ServiceConstants.USERNAME, Operator = Operators.Equals, Value = userName });
            param.Add(new CriteriaParam() { FieldName = ServiceConstants.PASSWORD, Operator = Operators.Equals, Value = password });
            param.Add(new CriteriaParam() { FieldName = ServiceConstants.RECORDSTATUS, Operator = Operators.Equals, Value = true });

            User user = UserDao.GetByCriteria(param).FirstOrDefault();

            if (user != null)
            {
                UserSession userSession = new UserSession();

                userSession.LoginDate = DateTime.Now;
                userSession.LoginStatus = 1;
                userSession.SessionId = Guid.NewGuid();
                userSession.User = user;                

                user.UserSessions.Add(userSession);

                UserSessionDao.Add(userSession);

                response.IsSuccess = true;

                UserDto userDto = Mapper.Map<UserDto>(user);

                response.Data = userDto;
            }
            else
            {
                ServiceException exception = new ServiceException("Username or password is wrong!");

                throw exception;
            }

            return response;
        }

        public ServiceResponse KillSession(ServiceRequest serviceParams)
        {
            ServiceResponse response = new ServiceResponse(false);

            List<CriteriaParam> param = new List<CriteriaParam>();

            string sessionId = serviceParams.Parameters[ServiceConstants.SESSIONID].ToString();

            param.Add(new CriteriaParam() { FieldName = ServiceConstants.SESSIONID, Operator = Operators.Equals, Value = sessionId });

            UserSession userSession = UserSessionDao.GetByCriteria(param).FirstOrDefault();

            if (userSession != null)
            {
                userSession.LogOffDate = DateTime.Now;
                userSession.LoginStatus = 0;

                UserSessionDao.Update(userSession);

                response.IsSuccess = true;
            }
            else
            {
                ServiceException exception = new ServiceException("Session doesn't exist");

                throw exception;
            }

            return response;
        }
    }
}
