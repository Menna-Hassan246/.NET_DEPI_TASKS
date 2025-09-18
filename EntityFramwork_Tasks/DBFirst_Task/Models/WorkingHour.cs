using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFCore.Models;

[PrimaryKey("Ssn", "Pnumber")]
[Table("Working_Hours")]
public partial class WorkingHour
{
    [Key]
    [Column("SSN")]
    public int Ssn { get; set; }

    [Key]
    [Column("PNumber")]
    public int Pnumber { get; set; }

    [Column("working_hr")]
    public int WorkingHr { get; set; }

    [ForeignKey("Pnumber")]
    [InverseProperty("WorkingHours")]
    public virtual Project PnumberNavigation { get; set; } = null!;

    [ForeignKey("Ssn")]
    [InverseProperty("WorkingHours")]
    public virtual Employee SsnNavigation { get; set; } = null!;
}
