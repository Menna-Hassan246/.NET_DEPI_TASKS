using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFCore.Models;

[Table("Department")]
public partial class Department
{
    [Key]
    [Column("DNum")]
    public int Dnum { get; set; }

    [Column("MANGRID")]
    public int? Mangrid { get; set; }

    [Column("DName")]
    [StringLength(255)]
    public string Dname { get; set; } = null!;

    [Column("Hire_Date")]
    public DateOnly HireDate { get; set; }

    [InverseProperty("DnumNavigation")]
    public virtual ICollection<DepartmentLocation> DepartmentLocations { get; set; } = new List<DepartmentLocation>();

    [InverseProperty("DnumNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [ForeignKey("Mangrid")]
    [InverseProperty("Departments")]
    public virtual Employee? Mangr { get; set; }

    [InverseProperty("DnumNavigation")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
