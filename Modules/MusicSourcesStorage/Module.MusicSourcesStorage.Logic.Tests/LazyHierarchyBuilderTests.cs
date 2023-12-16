using System.Diagnostics;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Tests;

public sealed class LazyHierarchyBuilderTests : HierarchyBuilderTestsBase
{
    protected override HierarchyMode HierarchyMode => HierarchyMode.Lazy;

    [Test]
    public void LargeNumberOfPathsBuiltSuccessfully()
    {
        var paths = CreateTooMuchPaths();

        var builder = CreateBuilder(HierarchyBuilderConfiguration.Default);
        var sw = Stopwatch.StartNew();
        builder.Build(paths, out _, out _);
        Assert.That(sw.Elapsed, Is.LessThan(TimeSpan.FromMilliseconds(100)));
    }
}