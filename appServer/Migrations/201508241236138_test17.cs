namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Travels", "fromCountryId", c => c.Int(nullable: false));
            AddColumn("dbo.Travels", "toCountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Travels", "fromCountryId");
            CreateIndex("dbo.Travels", "toCountryId");
            AddForeignKey("dbo.Travels", "fromCountryId", "dbo.Countries", "id");
            AddForeignKey("dbo.Travels", "toCountryId", "dbo.Countries", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Travels", "toCountryId", "dbo.Countries");
            DropForeignKey("dbo.Travels", "fromCountryId", "dbo.Countries");
            DropIndex("dbo.Travels", new[] { "toCountryId" });
            DropIndex("dbo.Travels", new[] { "fromCountryId" });
            DropColumn("dbo.Travels", "toCountryId");
            DropColumn("dbo.Travels", "fromCountryId");
        }
    }
}
