namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requestors", "ownerId", "dbo.AspNetUsers");
            DropIndex("dbo.Requestors", new[] { "ownerId" });
            AlterColumn("dbo.Requestors", "ownerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Requestors", "ownerId");
            AddForeignKey("dbo.Requestors", "ownerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requestors", "ownerId", "dbo.AspNetUsers");
            DropIndex("dbo.Requestors", new[] { "ownerId" });
            AlterColumn("dbo.Requestors", "ownerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Requestors", "ownerId");
            AddForeignKey("dbo.Requestors", "ownerId", "dbo.AspNetUsers", "Id");
        }
    }
}
