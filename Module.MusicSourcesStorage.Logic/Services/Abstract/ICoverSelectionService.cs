using System.Drawing;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.EventArgs;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ICoverSelectionService
{
    event EventHandler<CoverChangedEventArgs> CoverChanged;

    bool TryGetCover(int sourceId, string directoryRelativePath, out Image cover);

    Task<IActivableMultiStepTaskWithProgress<Void, Void>> CreateImageFileAsCoverSelectionTaskAsync(
        ImageFile imageFile,
        CancellationToken token = default
    );
}