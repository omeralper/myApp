namespace appServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Travels", "fromCountry", c => c.String());
            AlterColumn("dbo.Travels", "toCountry", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Travels", "toCountry", c => c.String(nullable: false));
            AlterColumn("dbo.Travels", "fromCountry", c => c.String(nullable: false));
        }
    }
}
