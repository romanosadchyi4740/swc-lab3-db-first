using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Pseudonym { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public override string ToString()
    {
        return
            $"{nameof(AuthorId)}: {AuthorId}, {nameof(FirstName)}: {FirstName}," +
            $" {nameof(LastName)}: {LastName}, {nameof(Pseudonym)}: {Pseudonym}, {nameof(Books)}: {Books}";
    }
}
