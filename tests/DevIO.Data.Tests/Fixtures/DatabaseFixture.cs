using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Tests.Fixtures;

public class DatabaseFixture : IDisposable
{
    public BusinessDbContext Context { get; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<BusinessDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .EnableSensitiveDataLogging()
            .Options;

        Context = new BusinessDbContext(options);
    }

    public async Task SeedAsync<TEntity>(IEnumerable<TEntity> entities) 
        where TEntity : class
    {
        Context.Set<TEntity>().AddRange(entities);
        await Context.SaveChangesAsync();
    }

    public async Task ClearAsync<TEntity>() where TEntity : class
    {
        Context.Set<TEntity>().RemoveRange(Context.Set<TEntity>());
        await Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Context?.Database.EnsureDeleted();
        Context?.Dispose();
    }
}