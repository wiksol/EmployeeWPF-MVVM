using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second.Database
{
    public class SecondDbContext : DbContext
    {
        public SecondDbContext(string connectionString) : base(connectionString)
        {
            if (!this.Database.Exists())
            {
                this.Database.Create();
            }
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
