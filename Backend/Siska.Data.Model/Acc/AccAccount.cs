using System;
using System.Text;
using System.Collections.Generic;


namespace Siska.Data.Model.Acc {
    
    public class AccAccount {
        public int AccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public int InsertBy { get; set; }
        public System.DateTime InsertDate { get; set; }
        public System.Nullable<int> UpdateBy { get; set; }
        public System.Nullable<System.DateTime> UpdateDate { get; set; }
        public bool RecordStatus { get; set; }
    }
}
