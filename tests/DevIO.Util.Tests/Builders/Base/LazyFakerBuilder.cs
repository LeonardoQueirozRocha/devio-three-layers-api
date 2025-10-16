using Bogus;

namespace DevIO.Util.Tests.Builders.Base;

public abstract class LazyFakerBuilder<TEntity> where TEntity : class
{
    protected const string Locale = "pt_BR";
    private readonly Lazy<Faker<TEntity>> _lazyFaker;
    private Faker<TEntity> Faker => _lazyFaker.Value;

    protected LazyFakerBuilder()
    {
        _lazyFaker = new Lazy<Faker<TEntity>>(Factory, isThreadSafe: true);
    }

    public virtual TEntity Build() =>
        Faker.Generate();

    public ICollection<TEntity> BuildCollection(int? count = null) =>
        Enumerable
            .Range(0, count ?? new Faker().Random.Number(5, 15))
            .Select(_ => Build())
            .ToArray();

    protected abstract Faker<TEntity> Factory();
}