using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFCore.Models;

[Table("Employee")]
public partial class Employee
{
    [Key]
    [Column("SSN")]
    public int Ssn { get; set; }

    [Column("MANGRID")]
    public int? Mangrid { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    [Column("FName")]
    [StringLength(255)]
    public string Fname { get; set; } = null!;

    [Column("LName")]
    [StringLength(255)]
    public string Lname { get; set; } = null!;

    [Column("DNum")]
    public int? Dnum { get; set; }

    [InverseProperty("Mangr")]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    [InverseProperty("SsnNavigation")]
    public virtual ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

    [ForeignKey("Dnum")]
    [InverseProperty("Employees")]
    public virtual Department? DnumNavigation { get; set; }

    [InverseProperty("Mangr")]
    public virtual ICollection<Employee> InverseMangr { get; set; } = new List<Employee>();

    [ForeignKey("Mangrid")]
    [InverseProperty("InverseMangr")]
    public virtual Employee? Mangr { get; set; }

    [InverseProperty("SsnNavigation")]
    public virtual ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();
}
