using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Exceptions;

namespace Module.MusicSourcesStorage.Gui.Extensions;

public static class EditMusicSourceAdditionalInfoContextExtensions
{
    public static void ValidateHasInitialAdditionalInfo(this IEditMusicSourceAdditionalInfoContext context)
    {
        if (context.InitialAdditionalInfo is null)
        {
            throw new ValidationException("Context has not additional info.");
        }
    }
}