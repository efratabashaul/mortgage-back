using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class Db : DbContext, IContext
    {
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<CustomerTasks>  CustomerTasks { get; set; }
        public DbSet<DocumentTypes> DocumentTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Leads> Leads { get; set; }

        public async Task save()
        {
            await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer("Data Source=DESKTOP-TP8FO8E\\SQLEXPRESS02;Initial Catalog=commemorative_project;Integrated Security=True;TrustServerCertificate=True");
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=mortgageDatabase;Integrated Security=False;User ID=sa;Password=mortgageTeam$100;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;");

        }
    }
}
