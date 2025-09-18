using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFCore.Models;

[PrimaryKey("Ssn", "Name")]
[Table("Dependent")]
public partial class Dependent
{
    [Key]
    [Column("SSN")]
    public int Ssn { get; set; }

    [Key]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string Gender { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    [ForeignKey("Ssn")]
    [InverseProperty("Dependents")]
    public virtual Employee SsnNavigation { get; set; } = null!;
}
