using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
