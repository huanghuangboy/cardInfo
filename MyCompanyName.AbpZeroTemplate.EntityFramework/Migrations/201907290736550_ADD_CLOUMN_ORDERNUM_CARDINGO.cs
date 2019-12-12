namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_CLOUMN_ORDERNUM_CARDINGO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Biz_CardInfo", "OrderNum", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Biz_CardInfo", "OrderNum");
        }
    }
}
