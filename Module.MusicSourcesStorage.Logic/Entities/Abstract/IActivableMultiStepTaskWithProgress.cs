namespace Module.MusicSourcesStorage.Logic.Entities.Abstract;

public interface IActivableMultiStepTaskWithProgress<TResult> : IMultiStepTaskWithProgress<TResult>
{
    void Activate(CancellationToken token = default);
}

public interface IActivableMultiStepTaskWithProgress<in TArgs, TResult> : IMultiStepTaskWithProgress<TResult>
{
    void Activate(TArgs args, CancellationToken token = default);
}