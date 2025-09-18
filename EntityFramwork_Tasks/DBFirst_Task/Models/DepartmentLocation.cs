using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFCore.Models;

[PrimaryKey("Dnum", "Location")]
[Table("Department_Locations")]
public partial class DepartmentLocation
{
    [Key]
    [Column("DNum")]
    public int Dnum { get; set; }

    [Key]
    [StringLength(255)]
    public string Location { get; set; } = null!;

    [ForeignKey("Dnum")]
    [InverseProperty("DepartmentLocations")]
    public virtual Department DnumNavigation { get; set; } = null!;
}
