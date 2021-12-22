using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Access
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Person> Person { get; set; }
    }
}