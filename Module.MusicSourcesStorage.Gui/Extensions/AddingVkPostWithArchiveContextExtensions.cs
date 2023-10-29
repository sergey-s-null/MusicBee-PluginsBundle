using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Exceptions;

namespace Module.MusicSourcesStorage.Gui.Extensions;

public static class AddingVkPostWithArchiveContextExtensions
{
    public static void ValidateHasPostId(this IAddingVkPostWithArchiveContext context)
    {
        if (context.PostId is null)
        {
            throw new ValidationException("Context has not post id.");
        }
    }

    public static void ValidateHasAttachedDocuments(this IAddingVkPostWithArchiveContext context)
    {
        if (context.AttachedDocuments is null)
        {
            throw new ValidationException("Context has not attached documents.");
        }
    }

    public static void ValidateHasSelectedDocument(this IAddingVkPostWithArchiveContext context)
    {
        if (context.SelectedDocument is null)
        {
            throw new ValidationException("Context has not selected document.");
        }
    }

    public static void ValidateHasIndexedFiles(this IAddingVkPostWithArchiveContext context)
    {
        if (context.IndexedFiles is null)
        {
            throw new ValidationException("Context has not indexed files.");
        }
    }

    public static void ValidateHasAdditionalInfo(this IAddingVkPostWithArchiveContext context)
    {
        if (context.AdditionalInfo is null)
        {
            throw new ValidationException("Context has not additional info.");
        }
    }
}