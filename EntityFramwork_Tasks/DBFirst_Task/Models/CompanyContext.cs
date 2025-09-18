using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFCore.Models;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentLocation> DepartmentLocations { get; set; }

    public virtual DbSet<Dependent> Dependents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<WorkingHour> WorkingHours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VPENOVD\\SQLEXPRESS;Database=Company;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Dnum).HasName("PK__Departme__24BE6B2F968BBBBC");

            entity.Property(e => e.Dnum).ValueGeneratedNever();
            entity.Property(e => e.Mangrid).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Mangr).WithMany(p => p.Departments).HasConstraintName("MANGE_DEP");
        });

        modelBuilder.Entity<DepartmentLocation>(entity =>
        {
            entity.HasKey(e => new { e.Dnum, e.Location }).HasName("PK__Departme__2AEBB89E81DF4537");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.DepartmentLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Department__DNum__403A8C7D");
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.HasKey(e => new { e.Ssn, e.Name }).HasName("PK__Dependen__AD29D672DACF4BE3");

            entity.Property(e => e.Gender).IsFixedLength();

            entity.HasOne(d => d.SsnNavigation).WithMany(p => p.Dependents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dependent__SSN__47DBAE45");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Ssn).HasName("PK__Employee__CA1E8E3D1671355B");

            entity.Property(e => e.Ssn).ValueGeneratedNever();
            entity.Property(e => e.Gender).IsFixedLength();
            entity.Property(e => e.Mangrid).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Employees).HasConstraintName("EMP_DEP");

            entity.HasOne(d => d.Mangr).WithMany(p => p.InverseMangr).HasConstraintName("FK_Manager");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Pnumber).HasName("PK__Project__DDE0878D93A4885F");

            entity.Property(e => e.Pnumber).ValueGeneratedNever();

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Projects).HasConstraintName("project_DEP");
        });

        modelBuilder.Entity<WorkingHour>(entity =>
        {
            entity.HasKey(e => new { e.Ssn, e.Pnumber }).HasName("PK__Working___07C08645120CB742");

            entity.HasOne(d => d.PnumberNavigation).WithMany(p => p.WorkingHours)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Working_H__PNumb__44FF419A");

            entity.HasOne(d => d.SsnNavigation).WithMany(p => p.WorkingHours)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Working_Hou__SSN__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
