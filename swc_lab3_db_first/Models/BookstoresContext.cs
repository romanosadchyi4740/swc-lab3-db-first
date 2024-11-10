using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace swc_lab3_db_first.Models;

public partial class BookstoresContext : DbContext
{
    public BookstoresContext()
    {
    }

    public BookstoresContext(DbContextOptions<BookstoresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookSalesView> BookSalesViews { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost; Database=bookstores; Username=postgres; Password=password");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("address_pkey");

            entity.ToTable("address");

            entity.HasIndex(e => e.CityId, "idx_city_id");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.BuildingNumber).HasColumnName("building_number");
            entity.Property(e => e.CityId)
                .ValueGeneratedOnAdd()
                .HasColumnName("city_id");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .HasColumnName("street");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("address_city_id_fkey");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("author_pkey");

            entity.ToTable("author");

            entity.HasIndex(e => e.LastName, "idx_author_hash").HasMethod("hash");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("last_name");
            entity.Property(e => e.Pseudonym)
                .HasColumnType("character varying")
                .HasColumnName("pseudonym");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("book_pkey");

            entity.ToTable("book");

            entity.HasIndex(e => e.Language, "idx_book_language");

            entity.HasIndex(e => new { e.Title, e.Language }, "idx_book_title_language");

            entity.HasIndex(e => e.PublisherId, "idx_publisher_id");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Language)
                .HasColumnType("character varying")
                .HasColumnName("language");
            entity.Property(e => e.NumberInStock).HasColumnName("number_in_stock");
            entity.Property(e => e.NumberInStore)
                .HasDefaultValue(0)
                .HasColumnName("number_in_store");
            entity.Property(e => e.PublisherId)
                .ValueGeneratedOnAdd()
                .HasColumnName("publisher_id");
            entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price");
            entity.Property(e => e.SellingPrice).HasColumnName("selling_price");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("book_publisher_id_fkey");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("book_author_author_id_fkey"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("book_author_book_id_fkey"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId").HasName("book_author_pkey");
                        j.ToTable("book_author");
                        j.IndexerProperty<int>("BookId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("book_id");
                        j.IndexerProperty<int>("AuthorId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("author_id");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("book_genre_genre_id_fkey"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("book_genre_book_id_fkey"),
                    j =>
                    {
                        j.HasKey("BookId", "GenreId").HasName("book_genre_pkey");
                        j.ToTable("book_genre");
                        j.IndexerProperty<int>("BookId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("book_id");
                        j.IndexerProperty<int>("GenreId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("genre_id");
                    });
        });

        modelBuilder.Entity<BookSalesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("book_sales_view");

            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.NumberOfCopiesSold).HasColumnName("number_of_copies_sold");
            entity.Property(e => e.Publisher)
                .HasColumnType("character varying")
                .HasColumnName("publisher");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
            entity.Property(e => e.TotalSales).HasColumnName("total_sales");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.City1)
                .HasColumnType("character varying")
                .HasColumnName("city");
            entity.Property(e => e.RegionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("region_id");

            entity.HasOne(d => d.Region).WithMany(p => p.Cities)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_region_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Country1)
                .HasColumnType("character varying")
                .HasColumnName("country");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.HasIndex(e => e.Email, "customer_email_key").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("genre_pkey");

            entity.ToTable("genre");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.GenreName)
                .HasColumnType("character varying")
                .HasColumnName("genre_name");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.BookId)
                .ValueGeneratedOnAdd()
                .HasColumnName("book_id");
            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("customer_id");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("payment_date");
            entity.Property(e => e.StaffId)
                .ValueGeneratedOnAdd()
                .HasColumnName("staff_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_customer_id_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_staff_id_fkey");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("publisher_pkey");

            entity.ToTable("publisher");

            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.Publisher1)
                .HasColumnType("character varying")
                .HasColumnName("publisher");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("region_pkey");

            entity.ToTable("region");

            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.CountryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("country_id");
            entity.Property(e => e.Region1)
                .HasColumnType("character varying")
                .HasColumnName("region");

            entity.HasOne(d => d.Country).WithMany(p => p.Regions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("region_country_id_fkey");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("staff_pkey");

            entity.ToTable("staff");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.StoreId)
                .ValueGeneratedOnAdd()
                .HasColumnName("store_id");

            entity.HasOne(d => d.Store).WithMany(p => p.Staff)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("staff_store_id_fkey");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("store_pkey");

            entity.ToTable("store");

            entity.HasIndex(e => e.AddressId, "idx_store_address");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.AddressId)
                .ValueGeneratedOnAdd()
                .HasColumnName("address_id");
            entity.Property(e => e.LastStockUpdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_stock_update");

            entity.HasOne(d => d.Address).WithMany(p => p.Stores)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("store_address_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
