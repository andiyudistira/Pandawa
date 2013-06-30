using System;
using System.Collections.Generic;
using System.Text;
using Iesi.Collections.Generic;

namespace Siska.Data.Model.Pos {
    
    public class User {
        public const string USER_MODEL_NAME = "User";
        public const string USER_TABLE_NAME = "Users";
        public const string USER_ID_FIELD = "UserId";
        public const string USER_NAME_FIELD = "UserName";
        public const string PASSWORD_FIELD = "Password";
        public const string RECORD_STATUS_FIELD = "RecordStatus";

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RecordStatus { get; set; }

        public ICollection<Role> Roles { get; set; }
        public ICollection<UserSession> UserSessions { get; set; }

        public User()
        {
            Roles = new HashedSet<Role>();
            UserSessions = new HashedSet<UserSession>();
        }
    }
}
