namespace Siska.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Siska.Core;
    using Siska.Data.Dao.Auth;
    using Siska.Data.Dto.Auth;
    using Siska.Data.Model.Auth;
    using System.Linq.Expressions;
    
    public class SessionService : ServiceBase, ISessionService
    {
        private IUserDao userDao;
        private IUserSessionDao userSessionDao;

        public SessionService(IUserDao userDao, IUserSessionDao userSessionDao)
        {
            this.userDao = userDao;
            this.userSessionDao = userSessionDao;
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

            Expression expr = (new List<IUser>()).Where(x =>
                x.UserName.Equals(userName) &&
                x.Password.Equals(password) &&
                x.RecordStatus).AsQueryable().Expression;

            var test = userDao.GetByCriteria(expr);
            IUser user = userDao.GetByCriteria(expr).FirstOrDefault();

            if (user != null)
            {
                IUserSession userSession = userSessionDao.CreateNew();

                userSession.LoginDate = DateTime.Now;
                userSession.LoginStatus = 1;
                userSession.SessionId = Guid.NewGuid().ToString();
                userSession.User = user;                

                user.UserSessions.Add(userSession);                

                response.IsSuccess = true;

                UserDto userDto = Mapper.Map<UserDto>(user);

                userSessionDao.SaveChanges();

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

            Expression expr = (new List<IUser>()).Where(x =>
                x.UserName.Equals(userName) &&
                x.Password.Equals(password) &&
                x.RecordStatus).AsQueryable().Expression;

            IUser user = userDao.GetByCriteria(expr).FirstOrDefault();

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

            Expression expr = (new List<IUserSession>()).Where(x =>
                    x.SessionId.Equals(sessionId)).AsQueryable().Expression;

            IUserSession userSession = userSessionDao.GetByCriteria(expr).FirstOrDefault();

            if (userSession != null)
            {
                userSession.LogOffDate = DateTime.Now;
                userSession.LoginStatus = 0;

                userSessionDao.SaveChanges();

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

            Expression expr = (new List<UserSession>()).Where(x =>
                    x.LoginStatus == (int)SEnvironment.Constants.LogonStatus.LoggedOn).AsQueryable().Expression;

            List<IUserSession> userSession = userSessionDao.GetByCriteria(expr).ToList();

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

            Expression expr = (new List<UserSession>()).Where(x =>
                    x.LoginStatus == (int)SEnvironment.Constants.LogonStatus.LoggedOn).AsQueryable().Expression;

            List<IUserSession> userSession = userSessionDao.GetByCriteria(expr).ToList();

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
