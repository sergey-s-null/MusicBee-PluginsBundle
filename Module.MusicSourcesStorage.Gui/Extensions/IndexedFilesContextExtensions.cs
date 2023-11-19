using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Exceptions;

namespace Module.MusicSourcesStorage.Gui.Extensions;

public static class IndexedFilesContextExtensions
{
    public static void ValidateHasIndexedFiles(this IIndexedFilesContext context)
    {
        if (context.IndexedFiles is null)
        {
            throw new ValidationException("Context has not indexed files.");
        }
    }
}