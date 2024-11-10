using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int? BuildingNumber { get; set; }

    public string? Street { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();

    public override string ToString()
    {
        return
            $"{nameof(AddressId)}: {AddressId}, {nameof(BuildingNumber)}: {BuildingNumber}," +
            $" {nameof(Street)}: {Street}, {nameof(CityId)}: {CityId}, {nameof(City)}: {City}," +
            $" {nameof(Stores)}: {Stores}";
    }
}
