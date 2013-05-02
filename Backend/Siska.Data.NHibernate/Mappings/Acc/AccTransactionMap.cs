using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Siska.Data.Model.Acc {
    
    
    public class AccTransactionMap : ClassMap<AccTransaction> {
        
        public AccTransactionMap() {
			Table("ACC_Transactions");
			LazyLoad();
			Id(x => x.TransactionId).GeneratedBy.Identity().Column("TransactionId");
			Map(x => x.TransactionDate).Column("TransactionDate").Not.Nullable().Length(8);
			Map(x => x.Total).Column("Total").Not.Nullable().Length(8);
			Map(x => x.Balance).Column("Balance").Not.Nullable().Length(8);
			Map(x => x.InsertBy).Column("InsertBy").Not.Nullable().Length(8);
			Map(x => x.InsertDate).Column("InsertDate").Not.Nullable().Length(8);
			Map(x => x.UpdateBy).Column("UpdateBy").Length(8);
			Map(x => x.UpdateDate).Column("UpdateDate").Length(8);
            HasMany(x => x.TransactionDetails).KeyColumns.Add("TransactionId").AsList().Inverse();
        }
    }
}
