namespace Siska.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Siska.Core;
    using Siska.Data.Dao;
    using Siska.Data.Dto.Pos;
    using Siska.Data.Model.Pos;
    
    public class SessionService : ServiceBase, ISessionService
    {
        public IUserDao UserDao { get; set; }

        public IUserSessionDao UserSessionDao { get; set; }

        public SessionService(IUserDao userDao)
        {
            UserDao = userDao;
        }

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
                ServiceException exception = new ServiceException(ErrorType.ErrorText(ErrorCode.SESSIONSERVICE_WRONG_USERNAME_PASSWORD));
                exception.ErrorCode = ErrorCode.SESSIONSERVICE_WRONG_USERNAME_PASSWORD;

                throw exception;
            }

            return response;
        }

        public ServiceResponse UnlockSession(ServiceRequest serviceParams)
        {
            ServiceResponse response = new ServiceResponse(false);

            List<CriteriaParam> param = new List<CriteriaParam>();

            string userName = serviceParams.Parameters[ServiceConstants.USERNAME].ToString();
            string password = serviceParams.Parameters[ServiceConstants.PASSWORD].ToString();

            param.Add(new CriteriaParam() { FieldName = ServiceConstants.USERNAME, Operator = Operators.Equals, Value = userName });
            param.Add(new CriteriaParam() { FieldName = ServiceConstants.PASSWORD, Operator = Operators.Equals, Value = password });
            param.Add(new CriteriaParam() { FieldName = ServiceConstants.RECORDSTATUS, Operator = Operators.Equals, Value = true });

            User user = UserDao.GetByCriteria(param).FirstOrDefault();

            if (user == null)
            {
                ServiceException exception = new ServiceException(ErrorType.ErrorText(ErrorCode.SESSIONSERVICE_WRONG_USERNAME_PASSWORD));
                exception.ErrorCode = ErrorCode.SESSIONSERVICE_WRONG_USERNAME_PASSWORD;

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
                ServiceException exception = new ServiceException(ErrorType.ErrorText(ErrorCode.SESSIONSERVICE_SESSION_DOESNT_EXIST));
                exception.ErrorCode = ErrorCode.SESSIONSERVICE_SESSION_DOESNT_EXIST;

                throw exception;
            }

            return response;
        }

        public ServiceResponse LogonStatus()
        {
            ServiceResponse response = new ServiceResponse(false);

            List<CriteriaParam> param = new List<CriteriaParam>();

            param.Add(new CriteriaParam() { FieldName = ServiceConstants.LOGONSTATUS, Operator = Operators.Equals, Value = (int)SEnvironment.Constants.LogonStatus.LoggedOn });

            List<UserSession> userSession = UserSessionDao.GetByCriteria(param).ToList();

            if (userSession != null && userSession.Count > 0)
            {
                response.IsSuccess = true;

                response.Data = SEnvironment.Constants.LogonStatus.LoggedOn;
            }
            else
            {
                response.IsSuccess = true;

                response.Data = SEnvironment.Constants.LogonStatus.LoggedOff;
            }

            return response;
        }

        public ServiceResponse LastLoggedOnUser()
        {
            ServiceResponse response = new ServiceResponse(false);

            List<CriteriaParam> param = new List<CriteriaParam>();

            param.Add(new CriteriaParam() { FieldName = ServiceConstants.LOGONSTATUS, Operator = Operators.Equals, Value = (int)SEnvironment.Constants.LogonStatus.LoggedOn });

            List<UserSession> userSession = UserSessionDao.GetByCriteria(param).ToList();

            if (userSession != null && userSession.Count > 0)
            {
                response.IsSuccess = true;

                UserSessionDto userSessionDto = Mapper.Map<UserSessionDto>(userSession.LastOrDefault());

                response.Data = userSessionDto;
            }
            else
            {
                ServiceException exception = new ServiceException(ErrorType.ErrorText(ErrorCode.SESSIONSERVICE_LAST_LOGGEDON_USER_DOESNT_EXIST));
                exception.ErrorCode = ErrorCode.SESSIONSERVICE_LAST_LOGGEDON_USER_DOESNT_EXIST;

                throw exception;
            }

            return response;
        }
    }
}
