using Microsoft.EntityFrameworkCore;

namespace EFCoreReferencesSample.DAL
{
    public class MainContext : DbContext
    {
        private static readonly string _connectionString =
            "User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=EfRelSample;";

        public DbSet<Person> Persons { get; set; }

        public DbSet<Pet> Pets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Person>()
                .HasData(new[]
                {
                    new Person
                    {
                        Id = 1,
                        Name = "John"
                    },
                    new Person
                    {
                        Id = 2,
                        Name = "Mike"
                    },
                    new Person
                    {
                        Id = 3,
                        Name = "Ron"
                    },
                    new Person
                    {
                        Id = 4,
                        Name = "Jennifer"
                    },
                });

            modelBuilder
                .Entity<Pet>()
                .HasData(new[]
                {
                    new Pet
                    {
                        Id = 1,
                        Name = "Scooby",
                    },
                    new Pet()
                    {
                        Id = 2,
                        Name = "Lessi"
                    }
                });
        }
    }
}
