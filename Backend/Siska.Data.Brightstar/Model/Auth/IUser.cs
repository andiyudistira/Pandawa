namespace Siska.Data.Model.Auth
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
        [InverseProperty("Users")]
        ICollection<IRole> Roles { get; set; }
        int UserId { get; set; }
        string UserName { get; set; }
        [InverseProperty("User")]
        ICollection<IUserSession> UserSessions { get; set; }
    }
}
