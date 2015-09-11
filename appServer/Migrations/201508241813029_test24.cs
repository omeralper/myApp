namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test24 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "estimatedVolume", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "estimatedVolume", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
