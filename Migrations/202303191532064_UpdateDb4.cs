namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotel", "Alias", c => c.String());
            AddColumn("dbo.Place", "Alias", c => c.String());
            AddColumn("dbo.TourCategory", "Alias", c => c.String());
            AddColumn("dbo.Vehicle", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicle", "Alias");
            DropColumn("dbo.TourCategory", "Alias");
            DropColumn("dbo.Place", "Alias");
            DropColumn("dbo.Hotel", "Alias");
        }
    }
}
