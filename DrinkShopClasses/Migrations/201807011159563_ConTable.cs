namespace DrinkShopClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CompanyName = c.String(),
                        Phone = c.Long(nullable: false),
                        Email = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
