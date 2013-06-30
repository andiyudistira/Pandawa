using System;
using Siska.Core;
using Siska.Data.Dao;

namespace Siska.Service
{
    public interface ISessionService
    {
        ServiceResponse KillSession(Siska.Core.ServiceRequest serviceParams);
        ServiceResponse StartSession(Siska.Core.ServiceRequest serviceParams);
        IUserDao UserDao { get; set; }
        IUserSessionDao UserSessionDao { get; set; }
    }
}
