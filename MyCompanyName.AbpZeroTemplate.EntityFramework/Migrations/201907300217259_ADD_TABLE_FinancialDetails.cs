namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_FinancialDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FinancialDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        PayType = c.Int(nullable: false),
                        Money = c.Int(nullable: false),
                        Desc = c.String(),
                        NowMoney = c.Int(nullable: false),
                        Mark = c.String(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FinancialDetails");
        }
    }
}
