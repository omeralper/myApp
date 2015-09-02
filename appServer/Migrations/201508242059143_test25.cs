namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test25 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Currencies", "name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Currencies", "name", c => c.Int(nullable: false));
        }
    }
}
