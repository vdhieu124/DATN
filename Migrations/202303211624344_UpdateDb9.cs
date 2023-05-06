namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tour", "DepartureTime", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tour", "DepartureTime", c => c.DateTime(nullable: false));
        }
    }
}
