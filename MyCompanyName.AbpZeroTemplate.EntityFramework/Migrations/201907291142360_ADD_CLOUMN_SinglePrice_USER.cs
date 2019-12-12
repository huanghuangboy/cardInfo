namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_CLOUMN_SinglePrice_USER : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "SinglePrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "SinglePrice");
        }
    }
}
