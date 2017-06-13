namespace Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REMOVED_FINISHDATE_TO_DESTINATION : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Destination", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Destination", "StartDate");
            DropColumn("dbo.Destination", "FinishDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Destination", "FinishDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Destination", "StartDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Destination", "Date");
        }
    }
}
