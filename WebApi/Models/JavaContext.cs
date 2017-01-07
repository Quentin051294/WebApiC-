using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JavaContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<CommandLine> CommandLines { get; set; }
        public DbSet<InfoCategory> InfoCategories { get; set; }
        public DbSet<InfoProduct> InfoProducts { get; set; }
        public DbSet<Language> Languages{ get; set; }
        public DbSet<Product> Products { get; set; }


        public JavaContext()
            : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JavaWebBD;")
        {

        }


        public override int SaveChanges()
        {
            foreach (DbEntityEntry<Customer> entry in ChangeTracker.Entries<Customer>().Where(u => u.State == EntityState.Modified))
                entry.Property("RowVersion").OriginalValue = entry.Property("RowVersion").CurrentValue;

            return base.SaveChanges();
        }
    }
}
