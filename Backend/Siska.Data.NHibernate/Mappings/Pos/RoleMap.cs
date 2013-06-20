using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Siska.Data.Model.Pos {
    
    
    public class RoleMap : ClassMap<Role> {
        
        public RoleMap() {
			Table("Roles");
			LazyLoad();
			Id(x => x.RoleId).GeneratedBy.Identity().Column("RoleId");
			Map(x => x.RoleName).Column("RoleName").Not.Nullable().Length(2147483647);
			Map(x => x.InsertDate).Column("InsertDate").Not.Nullable().Length(8);
			Map(x => x.UpdateDate).Column("UpdateDate").Length(8);
			Map(x => x.RecordStatus).Column("RecordStatus").Not.Nullable().Length(1);

            HasMany(x => x.UsersInRoles).KeyColumns.Add("UsersInRoleId").AsList().Inverse();

            References(x => x.InsertBy, "UserId").Fetch.Join();
            References(x => x.UpdateBy, "UserId").Nullable().Fetch.Join();
        }
    }
}
