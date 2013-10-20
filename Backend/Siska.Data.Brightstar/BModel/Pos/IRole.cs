namespace Siska.Data.BModel.Pos
{
    using System;
    using System.Collections.Generic;
    using BrightstarDB.EntityFramework;

    [Entity]
    public interface IRole
    {
        [Identifier]
        string Id { get; }
        IUser InsertBy { get; set; }
        DateTime InsertDate { get; set; }
        bool RecordStatus { get; set; }
        int RoleId { get; set; }
        string RoleName { get; set; }
        IUser UpdateBy { get; set; }
        DateTime? UpdateDate { get; set; }
        ICollection<IUser> Users { get; set; }
    }
}
