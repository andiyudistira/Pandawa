namespace Siska.Data.Model.Pos {

    using System.Collections.Generic;
    using Iesi.Collections.Generic;
    
    public class Role {
        public const string ROLE_MODEL_NAME = "Role";
        public const string ROLE_TABLE_NAME = "Roles";
        public const string ROLE_ID_FIELD = "RoleId";
        public const string ROLE_NAME_FIELD = "RoleName";
        public const string INSERT_BY_FIELD = "InsertBy";
        public const string INSERT_DATE_FIELD = "InsertDate";
        public const string UPDATE_BY_FIELD = "UpdateBy";
        public const string UPDATE_DATE_FIELD = "UpdateDate";
        public const string RECORD_STATUS_FIELD = "RecordStatus";

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
