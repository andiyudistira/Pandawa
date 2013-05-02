using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Siska.Data.Model.Acc {
    
    
    public class AccCategoryMap : ClassMap<AccCategory> {
        
        public AccCategoryMap() {
			Table("ACC_Categories");
			LazyLoad();
			Id(x => x.CategoryId).GeneratedBy.Identity().Column("CategoryId");
			//Map(x => x.AccountId).Column("AccountId").Not.Nullable().Length(8);
			Map(x => x.ParentId).Column("ParentId").Length(8);
			Map(x => x.CategoryName).Column("CategoryName").Not.Nullable().Length(2147483647);
			Map(x => x.Description).Column("Description").Length(2147483647);
			Map(x => x.InsertBy).Column("InsertBy").Not.Nullable().Length(8);
			Map(x => x.InsertDate).Column("InsertDate").Not.Nullable().Length(8);
			Map(x => x.UpdateBy).Column("UpdateBy").Length(8);
			Map(x => x.UpdateDate).Column("UpdateDate").Length(8);
			Map(x => x.RecordStatus).Column("RecordStatus").Not.Nullable().Length(1);
            References(x => x.Account, "AccountId").Fetch.Join();
            HasMany(x => x.TransactionDetails).KeyColumns.Add("TransactionId").AsList().Inverse();
        }
    }
}
