using Module.MusicSourcesStorage.Logic.Delegates;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ProgressCallbackConfiguration
{
    public RelativeProgressCallback? RelativeProgressCallback { get; set; }

    public Action<long>? AbsoluteProgressCallback { get; set; }
}