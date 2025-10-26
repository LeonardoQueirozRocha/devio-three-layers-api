using DevIO.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder
            .Property(product => product.Name)
            .IsRequired()
            .HasColumnType("VARCHAR(200)");

        builder
            .Property(product => product.Description)
            .IsRequired()
            .HasColumnType("VARCHAR(1000)");

        builder.ToTable("Products");
    }
}