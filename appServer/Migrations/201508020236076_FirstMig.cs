namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fromMessage = c.String(),
                        toMessage = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fromMessage = c.String(),
                        toMessage = c.String(),
                        body = c.String(maxLength: 2000),
                        readStatus = c.Int(nullable: false),
                        messageDate = c.DateTime(nullable: false),
                        Conversation_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Conversations", t => t.Conversation_id)
                .Index(t => t.Conversation_id);
            
            CreateTable(
                "dbo.Requestors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
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
            
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fromCity = c.String(),
                        fromCountry = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Conversation_id", "dbo.Conversations");
            DropIndex("dbo.Messages", new[] { "Conversation_id" });
            DropTable("dbo.Travels");
            DropTable("dbo.Requestors");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
        }
    }
}
