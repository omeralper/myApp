namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Travels", "ownerId", "dbo.AspNetUsers");
            DropIndex("dbo.Travels", new[] { "ownerId" });
            AlterColumn("dbo.Travels", "ownerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Travels", "ownerId");
            AddForeignKey("dbo.Travels", "ownerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Travels", "ownerId", "dbo.AspNetUsers");
            DropIndex("dbo.Travels", new[] { "ownerId" });
            AlterColumn("dbo.Travels", "ownerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Travels", "ownerId");
            AddForeignKey("dbo.Travels", "ownerId", "dbo.AspNetUsers", "Id");
        }
    }
}
