namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_CardIdCodes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardIdCodes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        Town = c.String(),
                        Area = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CardIdCodes");
        }
    }
}
