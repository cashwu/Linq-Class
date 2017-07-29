using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using EF01;
using EF01.Migrations;

namespace EF01
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CodeFirst : DbContext
    {
        // Your context has been configured to use a 'CodeFirst' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EF01.CodeFirst' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CodeFirst' 
        // connection string in the application configuration file.
        public CodeFirst()
            : base("name=CodeFirst")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CodeFirst, Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class Customer
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int Age { get; set; }
        public virtual ICollection<Addresses> Addresses { get; set; }
        public Customer()
        {
            Addresses = new List<Addresses>();
        }
    }

    public class Addresses
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [StringLength(150), Index]
        public string Address { get; set; }
        public int CustomerID { get; set; }
    }
}