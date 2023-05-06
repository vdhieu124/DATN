namespace DuLichV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hotel", "Detail", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Vehicle", "Detail", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Contact", "Message", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contact", "Message", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Vehicle", "Detail", c => c.String(maxLength: 200));
            AlterColumn("dbo.Hotel", "Detail", c => c.String(maxLength: 200));
        }
    }
}
