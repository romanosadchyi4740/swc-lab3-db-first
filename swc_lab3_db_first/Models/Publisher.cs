using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string? Publisher1 { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
