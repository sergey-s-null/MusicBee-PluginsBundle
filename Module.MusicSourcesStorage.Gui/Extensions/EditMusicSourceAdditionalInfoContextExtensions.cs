using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Exceptions;

namespace Module.MusicSourcesStorage.Gui.Extensions;

public static class EditMusicSourceAdditionalInfoContextExtensions
{
    public static void ValidateHasInitialAdditionalInfo(this IEditMusicSourceAdditionalInfoContext context)
    {
        if (context.InitialAdditionalInfo is null)
        {
            throw new ValidationException("Context has not initial additional info.");
        }
    }

    public static void ValidateHasEditedAdditionalInfo(this IEditMusicSourceAdditionalInfoContext context)
    {
        if (context.EditedAdditionalInfo is null)
        {
            throw new ValidationException("Context has not edited additional info.");
        }
    }
}