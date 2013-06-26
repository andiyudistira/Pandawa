using System;
using System.Text;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Siska.Data.Model.Pos {
    
    public class Role {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public User InsertBy { get; set; }
        public System.DateTime InsertDate { get; set; }
        public User UpdateBy { get; set; }
        public System.Nullable<System.DateTime> UpdateDate { get; set; }
        public bool RecordStatus { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new HashedSet<User>();
        }
    }
}
