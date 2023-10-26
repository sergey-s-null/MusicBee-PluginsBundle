using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class FileClassifier : IFileClassifier
{
    private static readonly IReadOnlyList<string> MusicFileExtensions = new[]
    {
        ".mp3",
        ".flac"
    };

    private static readonly IReadOnlyList<string> ImageFileExtensions = new[]
    {
        ".png",
        ".jpg",
        ".jpeg",
    };

    public FileType Classify(string filePath)
    {
        var extension = Path.GetExtension(filePath)?.ToLower();

        if (MusicFileExtensions.Contains(extension))
        {
            return FileType.MusicFile;
        }

        if (ImageFileExtensions.Contains(extension))
        {
            return FileType.Image;
        }

        return FileType.Unknown;
    }
}