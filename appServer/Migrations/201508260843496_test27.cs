namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test27 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Conversations", new[] { "TravelId" });
            DropIndex("dbo.Conversations", new[] { "RequestId" });
            CreateIndex("dbo.Conversations", "travelId");
            CreateIndex("dbo.Conversations", "requestId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Conversations", new[] { "requestId" });
            DropIndex("dbo.Conversations", new[] { "travelId" });
            CreateIndex("dbo.Conversations", "RequestId");
            CreateIndex("dbo.Conversations", "TravelId");
        }
    }
}
