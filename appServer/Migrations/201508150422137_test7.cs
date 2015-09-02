namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requestors", "ownerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Travels", "ownerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Requestors", "ownerId");
            CreateIndex("dbo.Travels", "ownerId");
            AddForeignKey("dbo.Requestors", "ownerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Travels", "ownerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Travels", "ownerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requestors", "ownerId", "dbo.AspNetUsers");
            DropIndex("dbo.Travels", new[] { "ownerId" });
            DropIndex("dbo.Requestors", new[] { "ownerId" });
            DropColumn("dbo.Travels", "ownerId");
            DropColumn("dbo.Requestors", "ownerId");
        }
    }
}
