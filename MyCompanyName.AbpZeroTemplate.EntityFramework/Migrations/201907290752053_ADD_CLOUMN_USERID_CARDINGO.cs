namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_CLOUMN_USERID_CARDINGO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Biz_CardInfo", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Biz_CardInfo", "Birthday", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Biz_CardInfo", "Birthday", c => c.DateTime(nullable: false));
            DropColumn("dbo.Biz_CardInfo", "UserId");
        }
    }
}
