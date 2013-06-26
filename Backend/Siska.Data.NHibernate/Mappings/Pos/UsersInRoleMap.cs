using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Siska.Data.Model.Pos
{
    public class UsersInRoleMap : ClassMap<UsersInRole>
    {
        public UsersInRoleMap()
        {
            Table("UsersInRoles");
			LazyLoad();
            Id(x => x.UsersInRoleId).GeneratedBy.Identity().Column("UsersInRoleId");
            References(x => x.User, "UserId");
            References(x => x.Role, "RoleId");
        }
    }
}
