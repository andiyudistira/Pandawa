using System;
using System.Text;
using System.Collections.Generic;


namespace Siska.Data.Model.Acc {
    
    public class AccTransaction {
        public int TransactionId { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public int Total { get; set; }
        public int Balance { get; set; }
        public int InsertBy { get; set; }
        public System.DateTime InsertDate { get; set; }
        public System.Nullable<int> UpdateBy { get; set; }
        public System.Nullable<System.DateTime> UpdateDate { get; set; }
        public IList<AccTransactionDetail> TransactionDetails { get; set; }
    }
}
