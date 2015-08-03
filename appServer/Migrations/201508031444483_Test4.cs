namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Travels", "itemType", c => c.Int());
            AlterColumn("dbo.Travels", "price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Travels", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Travels", "itemType", c => c.Int(nullable: false));
        }
    }
}
