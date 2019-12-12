namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_Column_RealName_ORDERDETAIL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "RealName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "RealName");
        }
    }
}
