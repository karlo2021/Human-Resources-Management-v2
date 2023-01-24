using HumanResourcesAPI.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HumanResourcesAPI.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext() : base()
    {
         
    }
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {

    }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>().HasKey(p => p.Id);
        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .IsRequired();

            entity.HasMany(e => e.Meetings)
                .WithOne(i => i.Person)
                .HasForeignKey(k => k.PersonId);
        });

        modelBuilder.Entity<Meeting>().HasKey(m => m.Id);
        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .IsRequired();
        });

        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, Name = "Karlo Bakota", Category = "Category 1", Birth = "2000-12-12",
                Rating = 4, Description = "Hello World" },
            new Person { Id = 2, Name = "Petar Mikulic", Category = "Category 1", Birth = "2000-12-12",
                Rating = 4, Description = "Hello World" },
            new Person { Id = 3, Name = "Tea Mikulic", Category = "Category 2", Birth = "2000-12-12",
                Rating = 4, Description = "Hello World" },
            new Person { Id = 4, Name = "Ivan Mikulic", Category = "Category 2", Birth = "2000-12-12", 
                Rating = 4, Description = "Hello World" },
            new Person { Id = 5, Name = "Anamarija Mikulic", Category = "Category 2", Birth = "2000-12-12",
                Rating = 4, Description = "Hello World" }
        );

        modelBuilder.Entity<Meeting>().HasData(
            new Meeting { Id = 1, Name = "Meeting 1", Address = "Location 1", Description = "Bad meeting",
                Rating = 2, InterviewDate = "2022-12-12", EmploymentDate = "NaN", Employed = false, 
                PersonId = 1},
            new Meeting { Id = 2, Name = "Meeting 2", Address = "Location 1", Description = "Good meeting",
                Rating = 4, InterviewDate = "2022-12-15", EmploymentDate = "2022-12-20", Employed = true,
                PersonId = 1
            },
            new Meeting { Id = 3, Name = "Meeting 1", Address = "Location 1", Description = "Good meeting",
                Rating = 5, InterviewDate = "2022-12-12", EmploymentDate = "2022-12-13", Employed = true,
                PersonId = 3
            },
            new Meeting { Id = 4, Name = "Meeting 1", Address = "Location 1", Description = "Good meeting",
                Rating = 3, InterviewDate = "2022-12-12", EmploymentDate = "2022-12-13", Employed = true,
                PersonId = 4
            },
            new Meeting { Id = 5, Name = "Meeting 1", Address = "Location 1", Description = "Good meeting",
                Rating = 5, InterviewDate = "2022-12-12", EmploymentDate = "2022-12-13", Employed = true,
                PersonId = 5
            }
        );

        modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
                );
    }
}
