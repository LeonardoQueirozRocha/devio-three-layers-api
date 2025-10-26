using DevIO.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Context;

public class BusinessDbContext : DbContext
{
    public BusinessDbContext(DbContextOptions<BusinessDbContext> options) : base(options)
    {
        // TODO: Another configs
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusinessDbContext).Assembly);

        var unmappedStringProperties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(entityType =>
                entityType
                    .GetProperties()
                    .Where(property => property.ClrType == typeof(string)));

        foreach (var unmappedStringProperty in unmappedStringProperties)
        {
            unmappedStringProperty.SetColumnType("VARCHAR(100)");
        }

        var relationships = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(entityType => entityType.GetForeignKeys());

        foreach (var relationship in relationships)
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        base.OnModelCreating(modelBuilder);
    }
}