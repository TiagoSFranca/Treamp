namespace Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDED_DATE_TO_NOTIFICATION : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notification", "Date");
        }
    }
}
