using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string? Country1 { get; set; }

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}
