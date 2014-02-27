namespace Siska.Data.Dto.Auth {

    using System;

    public class UserSessionDto {
        public int UserSessionId { get; set; }
        public Guid SessionId { get; set; }
        public UserDto User { get; set; }
        public DateTime LoginDate { get; set; }
        public System.Nullable<System.DateTime> LogOffDate { get; set; }
        public int LoginStatus { get; set; }
        public object Tag { get; set; }
    }
}
