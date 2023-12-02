using System.IO;

namespace Module.Core.Helpers;

public static class DirectoryHelper
{
    public static IReadOnlyCollection<string> GetFilesRecursively(string path)
    {
        return GetFilesInDirectory(path).ToReadOnlyCollection();
    }

    private static IEnumerable<string> GetFilesInDirectory(string path)
    {
        var files = Directory.GetFiles(path) as IEnumerable<string>;

        return Directory.GetDirectories(path)
            .Select(GetFilesInDirectory)
            .Aggregate(files, (first, second) => first.Concat(second));
    }

    public static bool TryCreateDirectory(string dirPath)
    {
        if (Directory.Exists(dirPath))
            return true;

        try
        {
            Directory.CreateDirectory(dirPath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void CreateIfNotExists(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public static void CreateForFile(string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (directory is null)
        {
            return;
        }

        Directory.CreateDirectory(directory);
    }

    /// <summary>
    /// Delete directory if it's empty.
    /// <br/>
    /// Delete all empty subdirectories if <paramref name="recursively"/> is set to true.
    /// </summary>
    public static void DeleteEmpty(string directory, bool recursively)
    {
        if (!Directory.Exists(directory))
        {
            return;
        }

        if (recursively)
        {
            var subDirectories = Directory.GetDirectories(directory);
            foreach (var subDirectory in subDirectories)
            {
                DeleteEmpty(subDirectory, true);
            }
        }

        if (IsEmpty(directory))
        {
            Directory.Delete(directory);
        }
    }

    public static bool IsEmpty(string directory)
    {
        return !Directory.EnumerateFileSystemEntries(directory).Any();
    }
}