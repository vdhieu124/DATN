namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb14 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookTourDetail", "CreatedBy");
            DropColumn("dbo.BookTourDetail", "CreatedDate");
            DropColumn("dbo.BookTourDetail", "ModifiedDate");
            DropColumn("dbo.BookTourDetail", "ModifiedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookTourDetail", "ModifiedBy", c => c.String());
            AddColumn("dbo.BookTourDetail", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookTourDetail", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookTourDetail", "CreatedBy", c => c.String());
        }
    }
}
