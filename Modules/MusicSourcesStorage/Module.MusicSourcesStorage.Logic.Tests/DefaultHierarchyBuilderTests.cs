using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Tests;

public sealed class DefaultHierarchyBuilderTests : HierarchyBuilderTestsBase
{
    protected override HierarchyMode HierarchyMode => HierarchyMode.Default;

    [Test]
    public void BuildingOfLargeNumberOfPathsIsNotPossible()
    {
        var paths = CreateTooMuchPaths();

        var builder = CreateBuilder();
        var task = Task.Run(() => builder.Build(paths, out _, out _));
        var completed = task.Wait(TimeSpan.FromMilliseconds(100));

        Assert.That(completed, Is.False);
    }
}