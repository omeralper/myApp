namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Travels", "finishDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Travels", "finishDate", c => c.DateTime(nullable: false));
        }
    }
}
