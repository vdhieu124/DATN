namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tour", "Detail", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tour", "Detail", c => c.String(maxLength: 4000));
        }
    }
}
