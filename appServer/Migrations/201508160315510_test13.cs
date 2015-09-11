namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "firstName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "firstName", c => c.String());
        }
    }
}
