namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tour", "TourCode", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tour", "TourCode", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
