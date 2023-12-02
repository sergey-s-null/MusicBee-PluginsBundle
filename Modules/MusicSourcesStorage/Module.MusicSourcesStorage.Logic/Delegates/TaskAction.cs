namespace Module.MusicSourcesStorage.Logic.Delegates;

public delegate void TaskAction<in TArgs>(
    TArgs args,
    RelativeProgressCallback progressCallback,
    CancellationToken token
);