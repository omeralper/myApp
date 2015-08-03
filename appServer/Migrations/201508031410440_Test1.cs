namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Travels", "toCity", c => c.String());
            AddColumn("dbo.Travels", "toCountry", c => c.String());
            AddColumn("dbo.Travels", "availableWeight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Travels", "availableVolume", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Travels", "startDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Travels", "finishtDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Travels", "itemType", c => c.Int(nullable: false));
            AddColumn("dbo.Travels", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Travels", "explanation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Travels", "explanation");
            DropColumn("dbo.Travels", "price");
            DropColumn("dbo.Travels", "itemType");
            DropColumn("dbo.Travels", "finishtDate");
            DropColumn("dbo.Travels", "startDate");
            DropColumn("dbo.Travels", "availableVolume");
            DropColumn("dbo.Travels", "availableWeight");
            DropColumn("dbo.Travels", "toCountry");
            DropColumn("dbo.Travels", "toCity");
        }
    }
}
