namespace Siska.Service
{
    using System;
    using Siska.Core;
    using Siska.Data.Dao;

    public interface ISessionService
    {
        ServiceResponse KillSession(Siska.Core.ServiceRequest serviceParams);
        ServiceResponse StartSession(Siska.Core.ServiceRequest serviceParams);
        ServiceResponse UnlockSession(ServiceRequest serviceParams);
        ServiceResponse LogonStatus();
        ServiceResponse LastLoggedOnUser();

        IUserDao UserDao { get; set; }
        IUserSessionDao UserSessionDao { get; set; }
    }
}
