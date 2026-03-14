namespace DataLayerGameOfLife.Repositories;

public sealed class RepositoryFactory
{
    private static readonly Lazy<RepositoryFactory> instance = new(() => new RepositoryFactory());
    public static RepositoryFactory Instance => instance.Value;

    private RepositoryFactory() { }

    public IInitialStateRepository GetInitialStateRepository()
        => new InitialStateRepository();
}
