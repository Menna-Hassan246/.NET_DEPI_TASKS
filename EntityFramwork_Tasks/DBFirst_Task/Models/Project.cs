using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFCore.Models;

[Table("Project")]
public partial class Project
{
    [Key]
    [Column("PNumber")]
    public int Pnumber { get; set; }

    [Column("DNum")]
    public int? Dnum { get; set; }

    [Column("PName")]
    [StringLength(255)]
    public string Pname { get; set; } = null!;

    [StringLength(255)]
    public string City { get; set; } = null!;

    [ForeignKey("Dnum")]
    [InverseProperty("Projects")]
    public virtual Department? DnumNavigation { get; set; }

    [InverseProperty("PnumberNavigation")]
    public virtual ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();
}
