using System.IO;
using Root.Helpers;
using Root.MusicBeeApi.Abstract;
using Root.Services.Abstract;

namespace Root.Services
{
    public class ResourceManager : IResourceManager
    {
        private readonly IMusicBeeApi _musicBeeApi;

        private readonly string _rootPath;

        public ResourceManager(IMusicBeeApi musicBeeApi)
        {
            _musicBeeApi = musicBeeApi;

            _rootPath = GetRootPath();
        }

        private string GetRootPath()
        {
            var dataPath = _musicBeeApi.Setting_GetPersistentStoragePath();

            return Path.Combine(dataPath, ResourcesHelper.RootDirectoryPath);
        }

        public void CreateRootIfNeeded()
        {
            if (!Directory.Exists(_rootPath))
            {
                Directory.CreateDirectory(_rootPath);
            }
        }

        public void DeleteRoot()
        {
            if (Directory.Exists(_rootPath))
            {
                Directory.Delete(_rootPath, true);
            }
        }

        public Stream? OpenRead(string resourceRelativePath)
        {
            var resourceFullPath = GetResourceFullPath(resourceRelativePath);

            return File.Exists(resourceFullPath)
                ? File.OpenRead(resourceFullPath)
                : null;
        }

        public Stream OpenWrite(string resourceRelativePath)
        {
            var fileInfo = new FileInfo(GetResourceFullPath(resourceRelativePath));

            if (fileInfo.Directory is not null && !fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            return File.Create(fileInfo.FullName);
        }

        private string GetResourceFullPath(string resourceRelativePath)
        {
            return Path.Combine(_rootPath, resourceRelativePath);
        }
    }
}