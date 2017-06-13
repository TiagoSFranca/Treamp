namespace Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDED_BIRTHDAY_AND_CPF : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Cpf", c => c.String(nullable: false, maxLength: 14));
            AddColumn("dbo.User", "BirthDay", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "BirthDay");
            DropColumn("dbo.User", "Cpf");
        }
    }
}
