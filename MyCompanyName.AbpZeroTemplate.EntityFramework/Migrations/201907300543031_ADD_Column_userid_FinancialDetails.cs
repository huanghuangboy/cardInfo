namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_Column_userid_FinancialDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinancialDetails", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.FinancialDetails", "UserId");
            AddForeignKey("dbo.FinancialDetails", "UserId", "dbo.AbpUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FinancialDetails", "UserId", "dbo.AbpUsers");
            DropIndex("dbo.FinancialDetails", new[] { "UserId" });
            DropColumn("dbo.FinancialDetails", "UserId");
        }
    }
}
