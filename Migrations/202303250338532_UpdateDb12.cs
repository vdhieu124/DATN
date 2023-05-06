namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookTour", "TypePayment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookTour", "TypePayment");
        }
    }
}
