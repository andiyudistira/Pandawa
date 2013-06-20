using System;
using System.Text;
using System.Collections.Generic;


namespace Siska.Data.Model.Pos {
    
    public class UserSession {
        public string SessionId { get; set; }
        public User User { get; set; }
        public DateTime LoginDate { get; set; }
        public System.Nullable<System.DateTime> LogOffDate { get; set; }
        public int LoginStatus { get; set; }
    }
}
