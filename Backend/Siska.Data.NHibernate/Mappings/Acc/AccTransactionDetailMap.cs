using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Siska.Data.Model.Acc {
    
    
    public class AccTransactionDetailMap : ClassMap<AccTransactionDetail> {
        
        public AccTransactionDetailMap() {
			Table("ACC_TransactionDetails");
			LazyLoad();
			Id(x => x.TransactionDetailsId).GeneratedBy.Identity().Column("TransactionDetailsId");
			//Map(x => x.TransactionId).Column("TransactionId").Not.Nullable().Length(8);
			//Map(x => x.CategoryId).Column("CategoryId").Not.Nullable().Length(8);
			Map(x => x.Increase).Column("Increase").Length(8);
			Map(x => x.Decrease).Column("Decrease").Length(8);
			Map(x => x.Description).Column("Description").Length(100);
			Map(x => x.InsertBy).Column("InsertBy").Not.Nullable().Length(8);
			Map(x => x.InsertDate).Column("InsertDate").Not.Nullable().Length(8);
			Map(x => x.UpdateBy).Column("UpdateBy").Length(8);
			Map(x => x.UpdateDate).Column("UpdateDate").Length(8);
            References(x => x.Category, "CategoryId").Fetch.Join();
            References(x => x.Transaction, "TransactionId").Fetch.Join();
        }
    }
}
