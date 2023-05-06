namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tour", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tour", "Image", c => c.String(nullable: false));
        }
    }
}
