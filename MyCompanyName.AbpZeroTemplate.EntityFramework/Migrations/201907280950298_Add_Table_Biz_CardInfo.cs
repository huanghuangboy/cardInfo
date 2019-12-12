namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_Biz_CardInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Biz_CardInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdCard = c.String(),
                        RealName = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        Prefecture = c.String(),
                        Address = c.String(),
                        Sex = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        AddrCode = c.String(),
                        LastCode = c.String(),
                        Status = c.String(),
                        StatusMsg = c.String(),
                        CheckTime = c.DateTime(),
                        Money = c.Int(nullable: false),
                        CurrMoney = c.Int(nullable: false),
                        RequestUrl = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        TenantId = c.Int(),
                        CreatorUserId = c.Long(),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Biz_CardInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AbpUsers", "NowMoney", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "NowMoney");
            DropTable("dbo.Biz_CardInfo",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Biz_CardInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
