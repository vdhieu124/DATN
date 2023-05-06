namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "OriginalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tour", "OriginalPrice");
        }
    }
}
