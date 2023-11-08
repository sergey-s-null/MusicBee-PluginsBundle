namespace Module.MusicSourcesStorage.Logic.Delegates;

public delegate T TaskFunction<out T>(
    RelativeProgressCallback progressCallback,
    CancellationToken token
);