namespace Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CHANGES : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TravelUser", "Pendent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Travel", "Pendent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Travel", "Finished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Travel", "Finished");
            DropColumn("dbo.Travel", "Pendent");
            DropColumn("dbo.TravelUser", "Pendent");
        }
    }
}
