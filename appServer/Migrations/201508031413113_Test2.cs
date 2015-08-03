namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Travels", "fromCity", c => c.String(nullable: false));
            AlterColumn("dbo.Travels", "fromCountry", c => c.String(nullable: false));
            AlterColumn("dbo.Travels", "toCity", c => c.String(nullable: false));
            AlterColumn("dbo.Travels", "toCountry", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Travels", "toCountry", c => c.String());
            AlterColumn("dbo.Travels", "toCity", c => c.String());
            AlterColumn("dbo.Travels", "fromCountry", c => c.String());
            AlterColumn("dbo.Travels", "fromCity", c => c.String());
        }
    }
}
