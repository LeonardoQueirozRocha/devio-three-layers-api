using DevIO.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings;

public class SupplierMapping : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(supplier => supplier.Id);

        builder
            .Property(supplier => supplier.Name)
            .IsRequired()
            .HasColumnType("VARCHAR(200)");

        builder
            .Property(supplier => supplier.Document)
            .IsRequired()
            .HasColumnType("VARCHAR(14)");

        builder
            .HasOne(supplier => supplier.Address)
            .WithOne(address => address.Supplier);

        builder
            .Property(supplier => supplier.SupplierType)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnType("VARCHAR(50)");

        builder
            .HasMany(supplier => supplier.Products)
            .WithOne(product => product.Supplier)
            .HasForeignKey(product => product.SupplierId);

        builder.ToTable("Suppliers");
    }
}