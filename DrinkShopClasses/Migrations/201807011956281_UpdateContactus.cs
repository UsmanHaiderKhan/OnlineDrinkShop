namespace DrinkShopClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateContactus : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Contacts", newName: "ContactUs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ContactUs", newName: "Contacts");
        }
    }
}
