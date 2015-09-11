namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test21 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Travels", new[] { "fromCityId" });
            DropIndex("dbo.Travels", new[] { "toCityId" });
            AlterColumn("dbo.Travels", "fromCityId", c => c.Int());
            AlterColumn("dbo.Travels", "toCityId", c => c.Int());
            CreateIndex("dbo.Travels", "fromCityId");
            CreateIndex("dbo.Travels", "toCityId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Travels", new[] { "toCityId" });
            DropIndex("dbo.Travels", new[] { "fromCityId" });
            AlterColumn("dbo.Travels", "toCityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Travels", "fromCityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Travels", "toCityId");
            CreateIndex("dbo.Travels", "fromCityId");
        }
    }
}
