using System;
using System.Text;
using System.Collections.Generic;


namespace Siska.Data.Dto.Pos {
    
    public class UserSessionDto {
        public int UserSessionId { get; set; }
        public Guid SessionId { get; set; }
        public UserDto User { get; set; }
        public DateTime LoginDate { get; set; }
        public System.Nullable<System.DateTime> LogOffDate { get; set; }
        public int LoginStatus { get; set; }
    }
}
