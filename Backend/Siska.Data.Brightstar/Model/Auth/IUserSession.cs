namespace Siska.Data.Model.Auth
{
    using System;
    using BrightstarDB.EntityFramework;

    [Entity]
    public interface IUserSession
    {
        [Identifier]
        string Id { get; }
        DateTime LoginDate { get; set; }
        int LoginStatus { get; set; }
        DateTime? LogOffDate { get; set; }
        string SessionId { get; set; }
        IUser User { get; set; }
        int UserSessionId { get; set; }
    }
}
