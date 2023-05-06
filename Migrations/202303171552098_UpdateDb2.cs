namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Category", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.News", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "IsActive");
            DropColumn("dbo.Category", "IsActive");
            DropColumn("dbo.Tour", "IsActive");
        }
    }
}
