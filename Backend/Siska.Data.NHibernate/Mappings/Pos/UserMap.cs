using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Siska.Data.Model.Pos {
    
    
    public class UserMap : ClassMap<User> {
        
        public UserMap() {
			Table("Users");
            LazyLoad();
			Id(x => x.UserId).GeneratedBy.Identity().Column("UserId");
			Map(x => x.UserName).Column("UserName").Not.Nullable().Length(2147483647);
			Map(x => x.Password).Column("Password").Not.Nullable().Length(2147483647);
			Map(x => x.RecordStatus).Column("RecordStatus").Not.Nullable().Length(1);

            HasMany(x => x.UserSessions).KeyColumns.Add("UserId").AsSet().Inverse();

            HasManyToMany<Role>(x => x.Roles).Table("UsersInRoles")
                                    .ParentKeyColumn("UserID")
                                    .ChildKeyColumn("RoleID")
                                    .AsSet()
                                    .Inverse()
                                    .LazyLoad();

            //HasMany(x => x.UsersInRoles).KeyColumns.Add("UserId").AsSet().Inverse();
        }
    }
}
