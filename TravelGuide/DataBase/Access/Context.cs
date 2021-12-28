using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Access
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Person> Person { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}