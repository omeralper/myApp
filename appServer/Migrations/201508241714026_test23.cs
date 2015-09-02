namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test23 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Requests", new[] { "toCityId" });
            AlterColumn("dbo.Requests", "toCityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "toCityId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Requests", new[] { "toCityId" });
            AlterColumn("dbo.Requests", "toCityId", c => c.Int());
            CreateIndex("dbo.Requests", "toCityId");
        }
    }
}
