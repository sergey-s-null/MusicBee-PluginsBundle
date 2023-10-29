using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Exceptions;

namespace Module.MusicSourcesStorage.Gui.Extensions;

public static class MusicSourceAdditionalInfoContextExtensions
{
    public static void ValidateHasAdditionalInfo(this IMusicSourceAdditionalInfoContext context)
    {
        if (context.AdditionalInfo is null)
        {
            throw new ValidationException("Context has not additional info.");
        }
    }
}