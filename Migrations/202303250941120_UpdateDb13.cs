namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb13 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BookTourDetail");
            AddColumn("dbo.BookTourDetail", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BookTourDetail", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.BookTourDetail");
            DropColumn("dbo.BookTourDetail", "Id");
            AddPrimaryKey("dbo.BookTourDetail", new[] { "BookTourId", "TourId" });
        }
    }
}
