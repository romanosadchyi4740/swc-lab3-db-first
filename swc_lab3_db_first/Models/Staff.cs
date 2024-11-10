using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int StoreId { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Store Store { get; set; } = null!;
}
