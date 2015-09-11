namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        countryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Countries", t => t.countryId, cascadeDelete: true)
                .Index(t => t.countryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "countryId", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "countryId" });
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
