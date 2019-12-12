namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Column_isApiData_To_OrderDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "IsApiData", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "IsApiData");
        }
    }
}
