using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class City
{
    public int CityId { get; set; }

    public string? City1 { get; set; }

    public int RegionId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Region Region { get; set; } = null!;

    public override string ToString()
    {
        return
            $"{nameof(CityId)}: {CityId}, {nameof(City1)}: {City1}, {nameof(RegionId)}: {RegionId}," +
            $" {nameof(Addresses)}: {Addresses}, {nameof(Region)}: {Region}";
    }
}
