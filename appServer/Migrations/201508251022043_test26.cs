namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test26 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Conversation_id", "dbo.Conversations");
            DropIndex("dbo.Messages", new[] { "Conversation_id" });
            RenameColumn(table: "dbo.Messages", name: "Conversation_id", newName: "conversationId");
            AddColumn("dbo.Conversations", "firstUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Conversations", "firstUserState", c => c.Int(nullable: false));
            AddColumn("dbo.Conversations", "secondUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Conversations", "secondUserState", c => c.Int(nullable: false));
            AddColumn("dbo.Conversations", "TravelId", c => c.Int());
            AddColumn("dbo.Conversations", "RequestId", c => c.Int());
            AddColumn("dbo.Messages", "fromUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Messages", "fromUserState", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "toUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Messages", "toUserState", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "messageBody", c => c.String(nullable: false, maxLength: 2000));
            AddColumn("dbo.Messages", "messageState", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "conversationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Conversations", "firstUserId");
            CreateIndex("dbo.Conversations", "secondUserId");
            CreateIndex("dbo.Conversations", "TravelId");
            CreateIndex("dbo.Conversations", "RequestId");
            CreateIndex("dbo.Messages", "fromUserId");
            CreateIndex("dbo.Messages", "toUserId");
            CreateIndex("dbo.Messages", "conversationId");
            AddForeignKey("dbo.Conversations", "firstUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "fromUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "toUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Conversations", "RequestId", "dbo.Requests", "id");
            AddForeignKey("dbo.Conversations", "secondUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Conversations", "TravelId", "dbo.Travels", "id");
            AddForeignKey("dbo.Messages", "conversationId", "dbo.Conversations", "id", cascadeDelete: true);
            DropColumn("dbo.Conversations", "fromMessage");
            DropColumn("dbo.Conversations", "toMessage");
            DropColumn("dbo.Messages", "fromMessage");
            DropColumn("dbo.Messages", "toMessage");
            DropColumn("dbo.Messages", "body");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "body", c => c.String(maxLength: 2000));
            AddColumn("dbo.Messages", "toMessage", c => c.String());
            AddColumn("dbo.Messages", "fromMessage", c => c.String());
            AddColumn("dbo.Conversations", "toMessage", c => c.String());
            AddColumn("dbo.Conversations", "fromMessage", c => c.String());
            DropForeignKey("dbo.Messages", "conversationId", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.Conversations", "secondUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Conversations", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.Messages", "toUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "fromUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Conversations", "firstUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "conversationId" });
            DropIndex("dbo.Messages", new[] { "toUserId" });
            DropIndex("dbo.Messages", new[] { "fromUserId" });
            DropIndex("dbo.Conversations", new[] { "RequestId" });
            DropIndex("dbo.Conversations", new[] { "TravelId" });
            DropIndex("dbo.Conversations", new[] { "secondUserId" });
            DropIndex("dbo.Conversations", new[] { "firstUserId" });
            AlterColumn("dbo.Messages", "conversationId", c => c.Int());
            DropColumn("dbo.Messages", "messageState");
            DropColumn("dbo.Messages", "messageBody");
            DropColumn("dbo.Messages", "toUserState");
            DropColumn("dbo.Messages", "toUserId");
            DropColumn("dbo.Messages", "fromUserState");
            DropColumn("dbo.Messages", "fromUserId");
            DropColumn("dbo.Conversations", "RequestId");
            DropColumn("dbo.Conversations", "TravelId");
            DropColumn("dbo.Conversations", "secondUserState");
            DropColumn("dbo.Conversations", "secondUserId");
            DropColumn("dbo.Conversations", "firstUserState");
            DropColumn("dbo.Conversations", "firstUserId");
            RenameColumn(table: "dbo.Messages", name: "conversationId", newName: "Conversation_id");
            CreateIndex("dbo.Messages", "Conversation_id");
            AddForeignKey("dbo.Messages", "Conversation_id", "dbo.Conversations", "id");
        }
    }
}
