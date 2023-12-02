namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public interface IActivableMultiStepTask<in TArgs, TResult> : IMultiStepTask<TResult>
{
    void Activate(TArgs args, CancellationToken token = default);
}