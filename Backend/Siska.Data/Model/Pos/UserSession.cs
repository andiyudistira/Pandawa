namespace Siska.Data.Model.Pos {

    using System;

    public class UserSession {
        public const string USERSESSION_MODEL_NAME = "User";
        public const string USERSESSION_TABLE_NAME = "Users";
        public const string USERSESSION_ID_FIELD = "UserId";
        public const string USER_FIELD = "User";
        public const string LOGIN_DATE_FIELD = "LoginDate";
        public const string LOGOFF_DATE_FIELD = "LogOffDate";
        public const string LOGIN_STATUS_FIELD = "LoginStatus";

        public int UserSessionId { get; set; }
        public Guid SessionId { get; set; }
        public User User { get; set; }
        public DateTime LoginDate { get; set; }
        public System.Nullable<System.DateTime> LogOffDate { get; set; }
        public int LoginStatus { get; set; }
    }
}
