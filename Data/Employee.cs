using System;
using System.Collections.Generic;

namespace Emp_management.Data;

public partial class Employee
{
    public int EmpId { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly DoJ { get; set; }

    public int Status { get; set; }

    public bool? IsDeleted { get; set; }
}
