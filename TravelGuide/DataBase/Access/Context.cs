using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Access
{
    public class Context : DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Аdvertisement> Advertisement { get; set; }
        public Context(DbContextOptions options) : base(options)
        {
            /*Database.EnsureCreated();*/
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 1, Name = "admin"
                },
                new Role()
                {
                    Id = 2, Name = "user"
                }
            );

            modelBuilder.Entity<Person>().HasData(
                new Person()
                {
                    Id = 1,
                    Email = "admin@admin",
                    PhoneNumber = "000",
                    Password = Hash.GetHash("Admin1Admin"),
                    RoleId = 1,
                }
            );
        }

    }
}