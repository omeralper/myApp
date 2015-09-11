namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requestors", "ownerId", "dbo.AspNetUsers");
            DropIndex("dbo.Requestors", new[] { "ownerId" });
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ownerId = c.String(nullable: false, maxLength: 128),
                        fromCityId = c.Int(),
                        fromCountryId = c.Int(nullable: false),
                        toCityId = c.Int(),
                        toCountryId = c.Int(nullable: false),
                        itemType = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        currencyId = c.Int(nullable: false),
                        estimatedVolume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        estimatedWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        title = c.String(nullable: false, maxLength: 100),
                        explanation = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Currencies", t => t.currencyId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.fromCityId)
                .ForeignKey("dbo.Countries", t => t.fromCountryId)
                .ForeignKey("dbo.AspNetUsers", t => t.ownerId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.toCityId)
                .ForeignKey("dbo.Countries", t => t.toCountryId)
                .Index(t => t.ownerId)
                .Index(t => t.fromCityId)
                .Index(t => t.fromCountryId)
                .Index(t => t.toCityId)
                .Index(t => t.toCountryId)
                .Index(t => t.currencyId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropTable("dbo.Requestors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Requestors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ownerId = c.String(nullable: false, maxLength: 128),
                        fromCity = c.String(),
                        fromCountry = c.String(),
                        toCity = c.String(),
                        toCountry = c.String(),
                        itemName = c.String(),
                        itemType = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        estimatedVolume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        estimatedWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        explanation = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.Requests", "toCountryId", "dbo.Countries");
            DropForeignKey("dbo.Requests", "toCityId", "dbo.Cities");
            DropForeignKey("dbo.Requests", "ownerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "fromCountryId", "dbo.Countries");
            DropForeignKey("dbo.Requests", "fromCityId", "dbo.Cities");
            DropForeignKey("dbo.Requests", "currencyId", "dbo.Currencies");
            DropIndex("dbo.Requests", new[] { "currencyId" });
            DropIndex("dbo.Requests", new[] { "toCountryId" });
            DropIndex("dbo.Requests", new[] { "toCityId" });
            DropIndex("dbo.Requests", new[] { "fromCountryId" });
            DropIndex("dbo.Requests", new[] { "fromCityId" });
            DropIndex("dbo.Requests", new[] { "ownerId" });
            DropTable("dbo.Currencies");
            DropTable("dbo.Requests");
            CreateIndex("dbo.Requestors", "ownerId");
            AddForeignKey("dbo.Requestors", "ownerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
