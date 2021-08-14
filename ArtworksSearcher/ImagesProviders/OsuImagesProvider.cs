using ArtworksSearcher.Ex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Root.Abstractions;

namespace ArtworksSearcher.ImagesProviders
{
    public class OsuImagesAsyncEnumerator : IAsyncEnumerator<BitmapImage>
    {
        private readonly string[] _imgExtensions = { ".jpg", ".png", ".jpeg" };

        private readonly DirectoryInfo _songsDir;
        private readonly string _query;
        private readonly long _minSize;

        private IEnumerator<FileInfo> _imagesEnumerator;

        public OsuImagesAsyncEnumerator(string songsDirPath, string query, long minSize = 0)
            : this(new DirectoryInfo(songsDirPath), query, minSize)
        { }

        public OsuImagesAsyncEnumerator(DirectoryInfo songsDir, string query, long minSize = 0)
        {
            _songsDir = songsDir;
            _query = query;
            _minSize = minSize;
        }

        #region IAsyncEnumerator

        private BitmapImage _current;
        public BitmapImage Current => _current;

        public async Task<bool> MoveNextAsync()
        {
            if (_imagesEnumerator is null)
                InitImagesEnumerator();

            while (true)
            {
                if (_imagesEnumerator.MoveNext())
                {
                    try
                    {
                        var data = File.ReadAllBytes(_imagesEnumerator.Current.FullName);
                        _current = new BitmapImage();
                        _current.BeginInit();
                        _current.StreamSource = new MemoryStream(data);
                        _current.EndInit();
                        return true;
                    }
                    catch { }
                }
                else
                    return false;
            }
        }

        private void InitImagesEnumerator()
        {
            var songsDirs = _songsDir.GetDirectories();
            var items = songsDirs.Select(songDir => new
            {
                SongDir = songDir,
                Coef = StringEx.CalcSimilarityCoef(_query, songDir.Name)
            }).ToArray();
            Array.Sort(items, (a, b) => b.Coef.CompareTo(a.Coef));

            _imagesEnumerator = items.SelectMany(item =>
            {
                return item.SongDir.GetFiles().SelectMany(songFile =>
                {
                    if (songFile.Length < _minSize)
                        return Array.Empty<FileInfo>();
                    if (!_imgExtensions.Contains(songFile.Extension.ToLower()))
                        return Array.Empty<FileInfo>();
                    return new[] {songFile};
                });
            }).GetEnumerator();
        }

        #endregion

    }

    public class OsuImagesProvider : IImagesProvider
    {
        private readonly string[] _imgExtensions = { ".jpg", ".png", ".jpeg" };
        private readonly DirectoryInfo _songsDir;
        public long MinSize = 0;

        public OsuImagesProvider(string songsDirPath)
        {
            _songsDir = new DirectoryInfo(songsDirPath);
        }

        public OsuImagesProvider(DirectoryInfo songsDir)
        {
            _songsDir = songsDir;
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
                Coef = StringEx.CalcSimilarityCoef(query, songDir.Name)
            }).ToArray();
            Array.Sort(items, (a, b) => b.Coef.CompareTo(a.Coef));

            foreach (var item in items)
            {
                foreach (var file in item.SongDir.GetFiles())
                {
                    if (file.Length < MinSize)
                        continue;
                    if (_imgExtensions.Contains(file.Extension.ToLower()))
                        yield return file;
                }
            }
        }

        public IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(string query)
        {
            return new OsuImagesAsyncEnumerator(_songsDir, query, MinSize);
        }

    }
}
