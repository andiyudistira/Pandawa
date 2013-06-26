using System;
using System.Text;
using System.Collections.Generic;


namespace Siska.Data.Model.Pos {
    
    public class UsersInRole {
        public int UsersInRoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
