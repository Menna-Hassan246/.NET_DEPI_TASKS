using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;

namespace Task1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure SSN as the primary key
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.SSN);
            // Configure FName as required with max length of 50
            modelBuilder.Entity<Employee>()
                .Property(e => e.FName)
                .IsRequired()
                .HasMaxLength(50);

            // Configure LName as required with max length of 50
            modelBuilder.Entity<Employee>()
                .Property(e => e.LName)
                .IsRequired()
                .HasMaxLength(50);

            // Configure Salary with precision (from previous question)
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
