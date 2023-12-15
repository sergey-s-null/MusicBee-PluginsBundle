using System.Text;
using Autofac;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Factories.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Tests;

public abstract class HierarchyBuilderTestsBase : TestsBase
{
    protected abstract HierarchyMode HierarchyMode { get; }

    [Test]
    public void BuilderFactoryResolved()
    {
        Container.ResolveKeyed<IHierarchyBuilderFactory>(HierarchyMode);
    }

    [Test]
    public void BuilderCreated()
    {
        CreateBuilder();
    }

    [Test]
    public void SimpleHierarchy()
    {
        var paths = new[]
        {
            "first/dir/file.txt",
            "root_file.exe",
            "first/dir/another.txt",
            "first/extra_file.txt",
        };

        var builder = CreateBuilder();
        builder.Build(paths, out var rootNodes, out var rootLeaves);

        Assert.Multiple(() =>
        {
            Assert.That(rootNodes, Has.Count.EqualTo(1));
            Assert.That(rootLeaves, Has.Count.EqualTo(1));
        });

        var firstNode = rootNodes[0];
        Assert.Multiple(() =>
        {
            Assert.That(firstNode.Path, Is.EqualTo(new[] { "first" }));
            Assert.That(firstNode.ChildNodes, Has.Count.EqualTo(1));
            Assert.That(firstNode.Leaves, Has.Count.EqualTo(1));
        });

        var dirNode = firstNode.ChildNodes[0];
        Assert.Multiple(() =>
        {
            Assert.That(dirNode.Path, Is.EqualTo(new[] { "first", "dir" }));
            Assert.That(dirNode.ChildNodes, Has.Count.EqualTo(0));
            Assert.That(dirNode.Leaves, Has.Count.EqualTo(2));
        });

        var file = dirNode.Leaves.First(x => x.Path.Last() == "file.txt");
        var another = dirNode.Leaves.First(x => x.Path.Last() == "another.txt");
        Assert.Multiple(() =>
        {
            Assert.That(file.Path, Is.EqualTo(new[] { "first", "dir", "file.txt" }));
            Assert.That(another.Path, Is.EqualTo(new[] { "first", "dir", "another.txt" }));
            Assert.That(file.Value, Is.EqualTo("first/dir/file.txt"));
            Assert.That(another.Value, Is.EqualTo("first/dir/another.txt"));
        });

        var extraFile = firstNode.Leaves[0];
        Assert.Multiple(() =>
        {
            Assert.That(extraFile.Path, Is.EqualTo(new[] { "first", "extra_file.txt" }));
            Assert.That(extraFile.Value, Is.EqualTo("first/extra_file.txt"));
        });

        var rootFile = rootLeaves[0];
        Assert.Multiple(() =>
        {
            Assert.That(rootFile.Path, Is.EqualTo(new[] { "root_file.exe" }));
            Assert.That(rootFile.Value, Is.EqualTo("root_file.exe"));
        });
    }

    [Test]
    public void DuplicatesInLeavesSkipped()
    {
        var paths = new[]
        {
            "file.txt",
            "file.TXT",
        };

        var builder = CreateBuilder();
        builder.Build(paths, out var rootNodes, out var rootLeaves);

        Assert.Multiple(() =>
        {
            Assert.That(rootNodes, Is.Empty);
            Assert.That(rootLeaves, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void DuplicatesInNodesSkipped()
    {
        var paths = new[]
        {
            "node/file.txt",
            "NOde/another.txt",
        };

        var builder = CreateBuilder();
        builder.Build(paths, out var rootNodes, out var rootLeaves);

        Assert.Multiple(() =>
        {
            Assert.That(rootLeaves, Is.Empty);
            Assert.That(rootNodes, Has.Count.EqualTo(1));
        });

        var node = rootNodes[0];
        Assert.That(node.Leaves, Has.Count.EqualTo(2));
    }

    [Test]
    public void PathToNodeDontBecameLeaf()
    {
        var paths = new[]
        {
            "node",
            "node/file.txt",
        };

        var builder = CreateBuilder();
        builder.Build(paths, out var rootNodes, out var rootLeaves);

        Assert.Multiple(() =>
        {
            Assert.That(rootNodes, Has.Count.EqualTo(1));
            Assert.That(rootLeaves, Is.Empty);
        });
    }

    protected IHierarchyBuilder<string, string> CreateBuilder()
    {
        var factory = Container.ResolveKeyed<IHierarchyBuilderFactory>(HierarchyMode);
        return factory.Create<string, string>(
            x => x.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
            StringComparer.InvariantCultureIgnoreCase
        );
    }

    protected static IReadOnlyList<string> CreateTooMuchPaths()
    {
        const string symbols = "abcdefghij";

        var paths = new List<string>();

        var builder = new StringBuilder();
        var random = new Random();
        for (var i = 0; i < 100000; i++)
        {
            var depth = random.Next(3, 10);
            for (var j = 0; j < depth; j++)
            {
                if (j > 0)
                {
                    builder.Append('/');
                }

                builder.Append(symbols[random.Next(0, symbols.Length)]);
            }

            paths.Add(builder.ToString());
            builder.Clear();
        }

        return paths;
    }
}