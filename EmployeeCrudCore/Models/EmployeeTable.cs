using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrudCore.Models;

[Table("EmployeeTable")]
public partial class EmployeeTable
{
    [Key]
    public int EmployeeId { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? Department { get; set; }

    public DateOnly? HireDate { get; set; }
}
