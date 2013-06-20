using System;
using System.Text;
using System.Collections.Generic;


namespace Siska.Data.Model.Pos {
    
    public class User {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RecordStatus { get; set; }

        public IList<UsersInRole> UsersInRoles { get; set; }
        public IList<UserSession> UserSessions { get; set; }
    }
}
