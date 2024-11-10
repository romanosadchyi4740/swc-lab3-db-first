using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int CustomerId { get; set; }

    public int StaffId { get; set; }

    public double? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int BookId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
