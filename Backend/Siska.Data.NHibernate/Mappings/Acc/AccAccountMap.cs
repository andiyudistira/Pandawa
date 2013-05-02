using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Siska.Data.Model.Acc {
    
    
    public class AccAccountMap : ClassMap<AccAccount> {
        
        public AccAccountMap() {
			Table("ACC_Accounts");
			LazyLoad();
			Id(x => x.AccountId).GeneratedBy.Identity().Column("AccountId");
			Map(x => x.AccountCode).Column("AccountCode").Not.Nullable().Length(2147483647);
			Map(x => x.AccountName).Column("AccountName").Not.Nullable().Length(2147483647);
			Map(x => x.Description).Column("Description").Length(2147483647);
			Map(x => x.InsertBy).Column("InsertBy").Not.Nullable().Length(8);
			Map(x => x.InsertDate).Column("InsertDate").Not.Nullable().Length(8);
			Map(x => x.UpdateBy).Column("UpdateBy").Length(8);
			Map(x => x.UpdateDate).Column("UpdateDate").Length(8);
			Map(x => x.RecordStatus).Column("RecordStatus").Not.Nullable().Length(1);
            HasMany(x => x.Categories).KeyColumns.Add("CategoryId").AsList().Inverse();
        }
    }
}
