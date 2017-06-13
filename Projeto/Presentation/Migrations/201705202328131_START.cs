namespace Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class START : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false, maxLength: 100),
                        District = c.String(nullable: false, maxLength: 45),
                        Number = c.String(nullable: false, maxLength: 10),
                        IdCity = c.Int(nullable: false),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: false)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 45),
                        LastName = c.String(nullable: false, maxLength: 45),
                        Password = c.String(nullable: false, maxLength: 64),
                        Email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        Agency = c.String(nullable: false, maxLength: 20),
                        Account = c.String(nullable: false, maxLength: 20),
                        IdUser = c.Int(nullable: false),
                        IdTypeAccount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeAccount", t => t.IdTypeAccount, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: false)
                .Index(t => t.IdUser)
                .Index(t => t.IdTypeAccount);
            
            CreateTable(
                "dbo.TypeAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 500),
                        Active = c.Boolean(nullable: false),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: false)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Phone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prefix = c.String(nullable: false, maxLength: 5),
                        Number = c.String(nullable: false, maxLength: 12),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: false)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pendent = c.Boolean(nullable: false),
                        IdRequester = c.Int(nullable: false),
                        IdRemittee = c.Int(nullable: false),
                        User_Id = c.Int(),
                        User_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdRemittee, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.IdRequester, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.User", t => t.User_Id1)
                .Index(t => t.IdRequester)
                .Index(t => t.IdRemittee)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.TravelUser",
                c => new
                    {
                        IdTravel = c.Int(nullable: false),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdTravel, t.IdUser })
                .ForeignKey("dbo.Travel", t => t.IdTravel, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: false)
                .Index(t => t.IdTravel)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Travel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: false)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Destination",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        FinishDate = c.DateTime(nullable: false, storeType: "date"),
                        IdTravel = c.Int(nullable: false),
                        IdCity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Travel", t => t.IdTravel, cascadeDelete: false)
                .Index(t => t.IdTravel);
            
            CreateTable(
                "dbo.TravelUserCost",
                c => new
                    {
                        IdTravelUserTravel = c.Int(nullable: false),
                        IdTravelUserUser = c.Int(nullable: false),
                        IdCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdTravelUserTravel, t.IdTravelUserUser, t.IdCost })
                .ForeignKey("dbo.Cost", t => t.IdCost, cascadeDelete: false)
                .ForeignKey("dbo.TravelUser", t => new { t.IdTravelUserTravel, t.IdTravelUserUser }, cascadeDelete: false)
                .Index(t => new { t.IdTravelUserTravel, t.IdTravelUserUser })
                .Index(t => t.IdCost);
            
            CreateTable(
                "dbo.Cost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        IdTypeCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeCost", t => t.IdTypeCost, cascadeDelete: false)
                .Index(t => t.IdTypeCost);
            
            CreateTable(
                "dbo.TypeCost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TravelUser", "IdUser", "dbo.User");
            DropForeignKey("dbo.TravelUserCost", new[] { "IdTravelUserTravel", "IdTravelUserUser" }, "dbo.TravelUser");
            DropForeignKey("dbo.TravelUserCost", "IdCost", "dbo.Cost");
            DropForeignKey("dbo.Cost", "IdTypeCost", "dbo.TypeCost");
            DropForeignKey("dbo.TravelUser", "IdTravel", "dbo.Travel");
            DropForeignKey("dbo.Travel", "IdUser", "dbo.User");
            DropForeignKey("dbo.Destination", "IdTravel", "dbo.Travel");
            DropForeignKey("dbo.Contact", "User_Id1", "dbo.User");
            DropForeignKey("dbo.Contact", "User_Id", "dbo.User");
            DropForeignKey("dbo.Contact", "IdRequester", "dbo.User");
            DropForeignKey("dbo.Contact", "IdRemittee", "dbo.User");
            DropForeignKey("dbo.Phone", "IdUser", "dbo.User");
            DropForeignKey("dbo.Notification", "IdUser", "dbo.User");
            DropForeignKey("dbo.BankAccount", "IdUser", "dbo.User");
            DropForeignKey("dbo.BankAccount", "IdTypeAccount", "dbo.TypeAccount");
            DropForeignKey("dbo.Address", "IdUser", "dbo.User");
            DropIndex("dbo.Cost", new[] { "IdTypeCost" });
            DropIndex("dbo.TravelUserCost", new[] { "IdCost" });
            DropIndex("dbo.TravelUserCost", new[] { "IdTravelUserTravel", "IdTravelUserUser" });
            DropIndex("dbo.Destination", new[] { "IdTravel" });
            DropIndex("dbo.Travel", new[] { "IdUser" });
            DropIndex("dbo.TravelUser", new[] { "IdUser" });
            DropIndex("dbo.TravelUser", new[] { "IdTravel" });
            DropIndex("dbo.Contact", new[] { "User_Id1" });
            DropIndex("dbo.Contact", new[] { "User_Id" });
            DropIndex("dbo.Contact", new[] { "IdRemittee" });
            DropIndex("dbo.Contact", new[] { "IdRequester" });
            DropIndex("dbo.Phone", new[] { "IdUser" });
            DropIndex("dbo.Notification", new[] { "IdUser" });
            DropIndex("dbo.BankAccount", new[] { "IdTypeAccount" });
            DropIndex("dbo.BankAccount", new[] { "IdUser" });
            DropIndex("dbo.Address", new[] { "IdUser" });
            DropTable("dbo.TypeCost");
            DropTable("dbo.Cost");
            DropTable("dbo.TravelUserCost");
            DropTable("dbo.Destination");
            DropTable("dbo.Travel");
            DropTable("dbo.TravelUser");
            DropTable("dbo.Contact");
            DropTable("dbo.Phone");
            DropTable("dbo.Notification");
            DropTable("dbo.TypeAccount");
            DropTable("dbo.BankAccount");
            DropTable("dbo.User");
            DropTable("dbo.Address");
        }
    }
}
