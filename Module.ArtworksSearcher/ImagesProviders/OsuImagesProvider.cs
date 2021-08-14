using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Helpers;
using Module.ArtworksSearcher.Settings;
using Root.Abstractions;

namespace Module.ArtworksSearcher.ImagesProviders
{
    public class OsuImagesProvider : IImagesProvider
    {
        private readonly string[] _imgExtensions = { ".jpg", ".png", ".jpeg" };
        private readonly DirectoryInfo _songsDir;
        private readonly long _minImageSizeInBytes;

        public OsuImagesProvider(IArtworksSearcherSettings settings)
        {
            _minImageSizeInBytes = settings.MinOsuImageByteSize;
            
            _songsDir = new DirectoryInfo(settings.OsuSongsDir);
        }

        public IEnumerable<BitmapImage> GetImagesIter(string query)
        {
            foreach (var data in GetBinaryIter(query))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(data);
                image.EndInit();
                yield return image;
            }
        }

        public IEnumerable<byte[]> GetBinaryIter(string query)
        {
            foreach (var file in GetFilesIter(query))
                yield return File.ReadAllBytes(file.FullName);
        }

        public IEnumerable<string> GetPathsIter(string query)
        {
            foreach (var file in GetFilesIter(query))
                yield return file.FullName;
        }

        public IEnumerable<FileInfo> GetFilesIter(string query)
        {
            var songsDirs = _songsDir.GetDirectories();
            var items = songsDirs.Select(songDir => new
            {
                SongDir = songDir,
                Coef = StringHelper.CalcSimilarityCoefficient(query, songDir.Name)
            }).ToArray();
            Array.Sort(items, (a, b) => b.Coef.CompareTo(a.Coef));

            foreach (var item in items)
            {
                foreach (var file in item.SongDir.GetFiles())
                {
                    if (file.Length < _minImageSizeInBytes)
                        continue;
                    if (_imgExtensions.Contains(file.Extension.ToLower()))
                        yield return file;
                }
            }
        }

        public IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(string query)
        {
            return new OsuImagesAsyncEnumerator(_songsDir, query, _minImageSizeInBytes);
        }

    }
}
