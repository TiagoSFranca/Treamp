namespace Presentation.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Util;

    internal sealed class Configuration : DbMigrationsConfiguration<Presentation.PresentationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Presentation.PresentationContext context)
        {
            context.TypeCost.AddOrUpdate(
                data => data.Id,
                new TypeCost()
                {
                    Id = ((int)TypeCostEnum.PERSONAL),
                    Name = "Pessoal"
                },
                new TypeCost()
                {
                    Id = ((int)TypeCostEnum.GROUP),
                    Name = "Coletivo"
                });
            context.TypeAccount.AddOrUpdate(
                data => data.Id,
                new TypeAccount()
                {
                    Id = ((int)TypeAccountEnum.CORRENTE),
                    Name = "Conta Corrente"
                },
                new TypeAccount()
                {
                    Id = ((int)TypeAccountEnum.POUPANcA),
                    Name = "Conta Poupança"
                });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
