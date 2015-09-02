namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test28 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "toUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "toUserId" });
            RenameColumn(table: "dbo.Messages", name: "fromUserId", newName: "ownerId");
            RenameIndex(table: "dbo.Messages", name: "IX_fromUserId", newName: "IX_ownerId");
            AddColumn("dbo.Messages", "ownerState", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "recieverState", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "fromUserState");
            DropColumn("dbo.Messages", "toUserId");
            DropColumn("dbo.Messages", "toUserState");
            DropColumn("dbo.Messages", "readStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "readStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "toUserState", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "toUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Messages", "fromUserState", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "recieverState");
            DropColumn("dbo.Messages", "ownerState");
            RenameIndex(table: "dbo.Messages", name: "IX_ownerId", newName: "IX_fromUserId");
            RenameColumn(table: "dbo.Messages", name: "ownerId", newName: "fromUserId");
            CreateIndex("dbo.Messages", "toUserId");
            AddForeignKey("dbo.Messages", "toUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
