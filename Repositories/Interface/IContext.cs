using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IContext
    {
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<CustomerTasks> CustomerTasks { get; set; }
        public DbSet<Leads> Leads { get; set; }
        public DbSet<DocumentTypes> DocumentTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public Task save();
    }
}
