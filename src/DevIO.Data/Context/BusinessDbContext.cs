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
}