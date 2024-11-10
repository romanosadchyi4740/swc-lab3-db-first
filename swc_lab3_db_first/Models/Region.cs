using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Region
{
    public int RegionId { get; set; }

    public string? Region1 { get; set; }

    public int CountryId { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country Country { get; set; } = null!;
}
