namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "Alias", c => c.String());
            AddColumn("dbo.Category", "Alias", c => c.String());
            AddColumn("dbo.News", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Alias");
            DropColumn("dbo.Category", "Alias");
            DropColumn("dbo.Tour", "Alias");
        }
    }
}
