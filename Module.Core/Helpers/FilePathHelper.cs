using System.IO;

namespace Module.Core.Helpers;

public static class FilePathHelper
{
    public static readonly IReadOnlyList<char> CommonInvalidChars = new[]
    {
        '"',
        '<',
        '>',
        '|',
        ':',
        '*',
        '?',
        '\\',
        '/',
    };

    public static bool HasInvalidChars(string filePath)
    {
        var invalidPathChars = Path.GetInvalidFileNameChars();
        return filePath.Any(x => invalidPathChars.Contains(x));
    }

    public static string ReplaceInvalidChars(string filePath, string replaceWith)
    {
        return string.Join(replaceWith, filePath.Split(Path.GetInvalidFileNameChars()));
    }
}