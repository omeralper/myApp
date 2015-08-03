namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Travels", "availableWeight", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Travels", "availableVolume", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Travels", "availableVolume", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Travels", "availableWeight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
