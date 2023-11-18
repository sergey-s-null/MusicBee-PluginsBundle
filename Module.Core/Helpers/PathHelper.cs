using System.IO;

namespace Module.Core.Helpers;

public static class PathHelper
{
    public static string GetRelativeToDirectoryPath(string relativeTo, string path)
    {
        // ToDirectoryPath вызывается для того, чтобы 
        //      "D:\_BIG_FILES_\Music Library"
        //      "D:\_BIG_FILES_\Music Library\Linkin Park"
        // вернуло "Linkin Park", а не "Music Library\Linkin Park"
        return GetRelativePath(ToDirectoryPath(relativeTo), path);
    }

    public static string GetRelativePath(string relativeTo, string path)
    {
        var relativeToUri = new Uri(relativeTo);
        var pathUri = new Uri(path);
        var relativeUri = relativeToUri.MakeRelativeUri(pathUri);

        return Uri
            .UnescapeDataString(relativeUri.ToString())
            .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
    }

    /// <summary>
    /// Добавляет в конец строки символ "/" разделения пути
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string ToDirectoryPath(string path)
    {
        if (!path.EndsWith(Path.DirectorySeparatorChar.ToString())
            && !path.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
        {
            return path + Path.DirectorySeparatorChar;
        }

        return path;
    }

    /// <summary>
    /// 1. Trim.<br/>
    /// 2. To lower.<br/>
    /// 3. Unify directory separator char.<br/>
    /// 4. Remove directory separator char duplications.<br/>
    /// 5. Add directory separator char at the end.
    /// </summary>
    public static string UnifyDirectoryPath(string directoryPath)
    {
        return UnifyPath(directoryPath, true);
    }

    /// <summary>
    /// 1. Trim.<br/>
    /// 2. To lower.<br/>
    /// 3. Unify directory separator char.<br/>
    /// 4. Remove directory separator char duplications.<br/>
    /// 5. Remove directory separator char at the end.
    /// </summary>
    public static string UnifyFilePath(string filePath)
    {
        return UnifyPath(filePath, false);
    }

    public static bool HasInvalidChars(string path)
    {
        var invalidPathChars = Path.GetInvalidPathChars();
        return path.Any(x => invalidPathChars.Contains(x));
    }

    // todo replace Characters -> Chars
    public static string ReplaceInvalidCharacters(string path, string replaceWith)
    {
        return string.Join(replaceWith, path.Split(Path.GetInvalidPathChars()));
    }

    private static string UnifyPath(string path, bool isDirectory)
    {
        var normalizedPath = path
            .Trim()
            .ToLower()
            .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

        normalizedPath = RemoveDoubleSeparators(normalizedPath);

        normalizedPath = isDirectory
            ? AddDirectorySeparatorCharAtTheEndIfNotPresent(normalizedPath)
            : RemoveDirectorySeparatorCharFromTheEndIfPresent(normalizedPath);

        return normalizedPath;
    }

    private static string RemoveDoubleSeparators(string path)
    {
        var doubleSeparator = $"{Path.DirectorySeparatorChar}{Path.DirectorySeparatorChar}";
        var singleSeparator = $"{Path.DirectorySeparatorChar}";
        while (path.Contains(doubleSeparator))
        {
            path = path.Replace(doubleSeparator, singleSeparator);
        }

        return path;
    }

    private static string AddDirectorySeparatorCharAtTheEndIfNotPresent(string path)
    {
        return path.EndsWith(Path.DirectorySeparatorChar.ToString())
            ? path
            : path + Path.DirectorySeparatorChar;
    }

    private static string RemoveDirectorySeparatorCharFromTheEndIfPresent(string path)
    {
        return path.EndsWith(Path.DirectorySeparatorChar.ToString())
            ? path.Substring(0, path.Length - 1)
            : path;
    }
}