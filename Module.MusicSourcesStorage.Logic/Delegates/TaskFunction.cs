namespace Module.MusicSourcesStorage.Logic.Delegates;

public delegate TResult TaskFunction<out TResult>(
    RelativeProgressCallback progressCallback,
    CancellationToken token
);

public delegate TResult TaskFunction<in TArgs, out TResult>(
    TArgs args,
    RelativeProgressCallback progressCallback,
    CancellationToken token
);