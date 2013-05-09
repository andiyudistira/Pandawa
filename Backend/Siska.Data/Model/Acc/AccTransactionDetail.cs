using System;
using System.Text;
using System.Collections.Generic;


namespace Siska.Data.Model.Acc {
    
    public class AccTransactionDetail {
        public int TransactionDetailsId { get; set; }
        //public int TransactionId { get; set; }
        //public int CategoryId { get; set; }
        public System.Nullable<int> Increase { get; set; }
        public System.Nullable<int> Decrease { get; set; }
        public string Description { get; set; }
        public int InsertBy { get; set; }
        public System.DateTime InsertDate { get; set; }
        public System.Nullable<int> UpdateBy { get; set; }
        public System.Nullable<System.DateTime> UpdateDate { get; set; }
        public AccTransaction Transaction { get; set; }
        public AccCategory Category { get; set; }
    }
}
