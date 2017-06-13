namespace Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CHANGES_IN_TRAVEL : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Travel", "IdUser", "dbo.User");
            DropIndex("dbo.Travel", new[] { "IdUser" });
            AddColumn("dbo.TravelUser", "IsOwner", c => c.Boolean(nullable: false));
            DropColumn("dbo.Travel", "IdUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Travel", "IdUser", c => c.Int(nullable: false));
            DropColumn("dbo.TravelUser", "IsOwner");
            CreateIndex("dbo.Travel", "IdUser");
            AddForeignKey("dbo.Travel", "IdUser", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
