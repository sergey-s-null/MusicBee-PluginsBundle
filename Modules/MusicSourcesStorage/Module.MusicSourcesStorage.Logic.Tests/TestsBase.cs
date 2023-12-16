using Autofac;

namespace Module.MusicSourcesStorage.Logic.Tests;

public abstract class TestsBase
{
    protected IContainer Container => _container
                                      ?? throw new InvalidOperationException("Container is not initialized.");

    private IContainer? _container;

    [SetUp]
    public virtual void Setup()
    {
        var builder = new ContainerBuilder();
        BuildContainer(builder);
        _container = builder.Build();
    }

    protected virtual void BuildContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<DIModule>();
    }
}