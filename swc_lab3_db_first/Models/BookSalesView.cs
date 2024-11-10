using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class BookSalesView
{
    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public double? TotalSales { get; set; }

    public long? NumberOfCopiesSold { get; set; }
}
