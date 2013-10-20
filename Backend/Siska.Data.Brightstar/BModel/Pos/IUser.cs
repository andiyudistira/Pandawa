namespace Siska.Data.BModel.Pos
{
    using System;
    using System.Collections.Generic;
    using BrightstarDB.EntityFramework;

    [Entity]
    public interface IUser
    {
        [Identifier]
        string Id { get; }
        string Password { get; set; }
        bool RecordStatus { get; set; }
        ICollection<IRole> Roles { get; set; }
        int UserId { get; set; }
        string UserName { get; set; }
        ICollection<IUserSession> UserSessions { get; set; }
    }
}
