using System;
using System.Collections.Generic;

namespace swc_lab3_db_first.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public double? PurchasePrice { get; set; }

    public double? SellingPrice { get; set; }

    public int PublisherId { get; set; }

    public int? NumberInStock { get; set; }

    public int? NumberInStore { get; set; }

    public string? Language { get; set; }

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public override string ToString()
    {
        return
            $"{nameof(BookId)}: {BookId}, {nameof(Title)}: {Title}, {nameof(PurchasePrice)}: {PurchasePrice}," +
            $" {nameof(SellingPrice)}: {SellingPrice}, {nameof(PublisherId)}: {PublisherId}, {nameof(NumberInStock)}:" +
            $" {NumberInStock}, {nameof(NumberInStore)}: {NumberInStore}, {nameof(Language)}: {Language}," +
            $" {nameof(Publisher)}: {Publisher}, {nameof(Authors)}: {Authors}, {nameof(Genres)}: {Genres}";
    }
}
