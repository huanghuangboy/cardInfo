namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_ORDERDETAIL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        OrderNum = c.String(),
                        CheckTime = c.DateTime(),
                        Money = c.Int(nullable: false),
                        CurrMoney = c.Int(nullable: false),
                        Status = c.String(),
                        StatusMsg = c.String(),
                        RequestUrl = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Biz_CardInfo", "UserId");
            DropColumn("dbo.Biz_CardInfo", "OrderNum");
            DropColumn("dbo.Biz_CardInfo", "Money");
            DropColumn("dbo.Biz_CardInfo", "CurrMoney");
            DropColumn("dbo.Biz_CardInfo", "RequestUrl");
            DropColumn("dbo.Biz_CardInfo", "TenantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Biz_CardInfo", "TenantId", c => c.Int());
            AddColumn("dbo.Biz_CardInfo", "RequestUrl", c => c.String());
            AddColumn("dbo.Biz_CardInfo", "CurrMoney", c => c.Int(nullable: false));
            AddColumn("dbo.Biz_CardInfo", "Money", c => c.Int(nullable: false));
            AddColumn("dbo.Biz_CardInfo", "OrderNum", c => c.String());
            AddColumn("dbo.Biz_CardInfo", "UserId", c => c.Long(nullable: false));
            DropForeignKey("dbo.OrderDetails", "UserId", "dbo.AbpUsers");
            DropIndex("dbo.OrderDetails", new[] { "UserId" });
            DropTable("dbo.OrderDetails");
        }
    }
}
