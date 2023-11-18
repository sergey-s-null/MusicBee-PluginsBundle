using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ISourceFilesRetargetingService
{
    Task RetargetAsync(
        int sourceId,
        MusicSourceAdditionalInfo oldAdditionalInfo,
        MusicSourceAdditionalInfo newAdditionalInfo,
        CancellationToken token = default
    );
}