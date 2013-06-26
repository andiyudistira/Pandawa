using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Siska.Data.Model.Pos {
    
    
    public class UserSessionMap : ClassMap<UserSession> 
    {
        
        public UserSessionMap() {
			Table("UserSession");
            LazyLoad();
            Id(x => x.UserSessionId).GeneratedBy.Identity().Column("UserSessionId");            
            References(x => x.User, "UserId");
            Map(x => x.SessionId).Column("SessionId");
			Map(x => x.LoginDate).Column("LoginDate").Length(8);
			Map(x => x.LogOffDate).Column("LogOffDate").Length(8);
			Map(x => x.LoginStatus).Column("LoginStatus").Not.Nullable().Length(8);
        }
    }
}
