using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            var ensureDLLIsCopied = SqlProviderServices.Instance;
        }

        public AppDbContext(string connectionString) : base(connectionString)
        {
            ConfigureContext();
        }

        public AppDbContext() : base("name=SOLVE")
        {
            ConfigureContext();
        }

        private void ConfigureContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
        }

        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Worker>().ToTable("Workers");
            modelBuilder.Entity<Worker>().HasKey(w => w.Id);
            modelBuilder.Entity<Worker>().Property(w => w.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Worker>().Property(w => w.Age).IsRequired();
            modelBuilder.Entity<Worker>().Property(w => w.Salary).IsRequired();
            modelBuilder.Entity<Worker>().Property(w => w.Specialization).IsRequired();
        }
    }
}