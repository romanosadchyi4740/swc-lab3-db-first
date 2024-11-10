using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public DateTime? LastStockUpdate { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
