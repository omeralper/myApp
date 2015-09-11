namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Travels", "fromCityId", c => c.Int(nullable: false));
            AddColumn("dbo.Travels", "toCityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Travels", "fromCityId");
            CreateIndex("dbo.Travels", "toCityId");
            AddForeignKey("dbo.Travels", "fromCityId", "dbo.Cities", "id");
            AddForeignKey("dbo.Travels", "toCityId", "dbo.Cities", "id");
            DropColumn("dbo.Travels", "fromCity");
            DropColumn("dbo.Travels", "fromCountry");
            DropColumn("dbo.Travels", "toCity");
            DropColumn("dbo.Travels", "toCountry");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Travels", "toCountry", c => c.String());
            AddColumn("dbo.Travels", "toCity", c => c.String(nullable: false));
            AddColumn("dbo.Travels", "fromCountry", c => c.String());
            AddColumn("dbo.Travels", "fromCity", c => c.String(nullable: false));
            DropForeignKey("dbo.Travels", "toCityId", "dbo.Cities");
            DropForeignKey("dbo.Travels", "fromCityId", "dbo.Cities");
            DropIndex("dbo.Travels", new[] { "toCityId" });
            DropIndex("dbo.Travels", new[] { "fromCityId" });
            DropColumn("dbo.Travels", "toCityId");
            DropColumn("dbo.Travels", "fromCityId");
        }
    }
}
