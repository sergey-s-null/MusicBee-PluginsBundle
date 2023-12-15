using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Tests;

public sealed class LazyHierarchyBuilderTests : HierarchyBuilderTestsBase
{
    protected override HierarchyMode HierarchyMode => HierarchyMode.Lazy;

    [Test]
    [Timeout(1000)]
    public void LargeNumberOfPathsBuiltSuccessfully()
    {
        var paths = CreateTooMuchPaths();

        var builder = CreateBuilder();
        builder.Build(paths, out _, out _);
    }
}