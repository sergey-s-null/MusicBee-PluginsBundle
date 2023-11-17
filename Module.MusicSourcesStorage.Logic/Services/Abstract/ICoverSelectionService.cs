using System.Drawing;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.EventArgs;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ICoverSelectionService
{
    event EventHandler<CoverChangedEventArgs> CoverChanged;

    Task<Image?> GetCoverAsync(
        int sourceId,
        string directoryRelativePath,
        CancellationToken token = default
    );

    Task<IActivableMultiStepTask<CoverSelectionArgs, Void>> CreateCoverSelectionTaskAsync(
        int imageFileId,
        CancellationToken token = default
    );

    Task<IActivableMultiStepTask<CoverSelectionArgs, Void>> CreateCoverSelectionTaskAsync(
        ImageFile imageFile,
        CancellationToken token = default
    );
}