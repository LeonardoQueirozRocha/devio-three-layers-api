using DevIO.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings;

public class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(address => address.Id);

        builder
            .Property(address => address.Street)
            .IsRequired()
            .HasColumnType("VARCHAR(200)");

        builder
            .Property(address => address.Number)
            .IsRequired()
            .HasColumnType("VARCHAR(50)");

        builder
            .Property(address => address.Complement)
            .HasColumnType("VARCHAR(250)");

        builder
            .Property(address => address.ZipCode)
            .IsRequired()
            .HasColumnType("VARCHAR(8)");

        builder
            .Property(address => address.Neighborhood)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");

        builder
            .Property(address => address.City)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");

        builder
            .Property(address => address.State)
            .IsRequired()
            .HasColumnType("VARCHAR(50)");
        
        builder.ToTable("Addresses");
    }
}