namespace Module.Core.Helpers
{
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
    }
}