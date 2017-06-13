namespace WebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FIRST : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        IdState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("State", t => t.IdState, cascadeDelete: true)
                .Index(t => t.IdState);
            
            CreateTable(
                "State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        Abbreviation = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("City", "IdState", "State");
            DropIndex("City", new[] { "IdState" });
            DropTable("State");
            DropTable("City");
        }
    }
}
