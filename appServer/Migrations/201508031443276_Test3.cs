namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Travels", "finishDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Travels", "finishtDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Travels", "finishtDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Travels", "finishDate");
        }
    }
}
