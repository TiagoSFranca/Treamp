namespace Presentation
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PresentationContext : DbContext
    {
        // Your context has been configured to use a 'PresentationContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Presentation.PresentationContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PresentationContext' 
        // connection string in the application configuration file.
        public PresentationContext()
            : base("name=PresentationContext")
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<BankAccount> BankAccount { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Cost> Cost { get; set; }
        public virtual DbSet<Destination> Destination { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Travel> Travel { get; set; }
        public virtual DbSet<TravelUser> TravelUser { get; set; }
        public virtual DbSet<TravelUserCost> TravelUserCost { get; set; }
        public virtual DbSet<TypeAccount> TypeAccount { get; set; }
        public virtual DbSet<TypeCost> TypeCost { get; set; }
        public virtual DbSet<User> User { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}