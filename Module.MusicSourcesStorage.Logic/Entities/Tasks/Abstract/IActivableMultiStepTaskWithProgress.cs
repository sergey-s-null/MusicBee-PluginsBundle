namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

// todo rename without "WithProgress"
public interface IActivableMultiStepTaskWithProgress<in TArgs, TResult> : IMultiStepTaskWithProgress<TResult>
{
    void Activate(TArgs args, CancellationToken token = default);
}