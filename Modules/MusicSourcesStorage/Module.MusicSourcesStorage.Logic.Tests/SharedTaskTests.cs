using Module.MusicSourcesStorage.Logic.Entities.Tasks;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Factories;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Tests;

public sealed class SharedTaskTests
{
    [Test]
    public void NotActivatedByDefault()
    {
        var task = new SharedTask<int>(() => ActivableTaskFactory.FromResult(42));
        Assert.That(task.IsActivated, Is.False);
    }

    [Test]
    public void InternalTaskNotCreatedOnSharedTaskCreation()
    {
        var internalTaskCreated = false;
        _ = new SharedTask<int>(() =>
        {
            internalTaskCreated = true;
            return ActivableTaskFactory.FromResult(42);
        });

        Assert.That(internalTaskCreated, Is.False);
    }

    [Test]
    public async Task InternalTaskCreatedOnSharedTaskActivated()
    {
        var internalTaskCreated = false;
        var task = new SharedTask<int>(() =>
        {
            internalTaskCreated = true;
            return ActivableTaskFactory.FromResult(42);
        });

        task.Activate(Void.Instance);
        await task.Task;

        Assert.That(internalTaskCreated, Is.True);
    }

    [Test]
    public async Task SuccessfullyCompletedWithoutAcquiring()
    {
        var task = new SharedTask<int>(() => ActivableTaskFactory.FromResult(42));
        task.Activate(Void.Instance);
        await task.Task;
    }

    [Test]
    public async Task SuccessfullyCompletedWhenActivatedAfterAcquiring()
    {
        var task = new SharedTask<int>(() => ActivableTaskFactory.FromResult(42));
        task.Acquire();
        task.Activate(Void.Instance);
        await task.Task;
    }

    [Test]
    public async Task SuccessfullyCompletedWhenAcquiredAfterActivating()
    {
        var task = new SharedTask<int>(() => ActivableTaskFactory.FromResult(42));
        task.Activate(Void.Instance);
        task.Acquire();
        await task.Task;
    }

    [Test]
    public void CancelledAfterLastRelease()
    {
        var internalTaskCancelled = false;
        var sharedTaskCancelled = false;

        IActivableTask<Void, int>? internalTask = null;

        var sharedTask = new SharedTask<int>(() =>
        {
            internalTask = ActivableTaskFactory.CreateWithoutArgs(token =>
            {
                Task.Delay(TimeSpan.FromDays(1), token).Wait(token);
                return 42;
            });

            internalTask.Cancelled += (_, _) => internalTaskCancelled = true;

            return internalTask;
        });

        sharedTask.Cancelled += (_, _) => sharedTaskCancelled = true;

        sharedTask.Acquire();
        sharedTask.Activate(Void.Instance);
        sharedTask.Release();

        Assert.Multiple(() =>
        {
            Assert.Throws<AggregateException>(() => sharedTask.Task.Wait());
            Assert.That(() => sharedTaskCancelled, Is.True.After(1000, 50));
            Assert.That(internalTask, Is.Not.Null);
            Assert.Throws<AggregateException>(() => internalTask?.Task.Wait());
            Assert.That(internalTaskCancelled, Is.True);
        });
    }

    [Test]
    public void NotCancelledWhenStillAcquiredAfterRelease()
    {
        var internalTaskCancelled = false;
        var sharedTaskCancelled = false;

        var lockObj = new SemaphoreSlim(0, 1);

        var sharedTask = new SharedTask<int>(() =>
        {
            var internalTask = ActivableTaskFactory.CreateWithoutArgs(() =>
            {
                lockObj.Wait();
                return 42;
            });

            internalTask.Cancelled += (_, _) => internalTaskCancelled = true;

            return internalTask;
        });

        sharedTask.Cancelled += (_, _) => sharedTaskCancelled = true;

        sharedTask.Activate(Void.Instance);

        for (var i = 0; i < 10; i++)
        {
            sharedTask.Acquire();
        }

        for (var i = 0; i < 9; i++)
        {
            sharedTask.Release();
        }

        lockObj.Release();

        Assert.Multiple(async () =>
        {
            Assert.That(await sharedTask.Task, Is.EqualTo(42));
            Assert.That(sharedTaskCancelled, Is.False);
            Assert.That(internalTaskCancelled, Is.False);
        });
    }
}