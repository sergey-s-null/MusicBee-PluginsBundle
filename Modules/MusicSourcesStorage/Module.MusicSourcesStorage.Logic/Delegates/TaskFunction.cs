namespace Module.MusicSourcesStorage.Logic.Delegates;

public delegate TResult TaskFunction<in TArgs, out TResult>(
    TArgs args,
    RelativeProgressCallback progressCallback,
    CancellationToken token
);