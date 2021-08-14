using ArtworksSearcher.Ex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ArtworksSearcher.ImagesProviders
{
    public class OsuImagesAsyncEnumerator : IAsyncEnumerator<BitmapImage>
    {
        private readonly string[] _imgExtensions = { ".jpg", ".png", ".jpeg" };

        private DirectoryInfo _songsDir;
        private string _query;
        private long _minSize;

        private IEnumerator<FileInfo> _imagesEnumerator = null;

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

        public async Task<bool> MoveNext()
        {
            if (_imagesEnumerator is null)
                InitImagesEnumerator();

            while (true)
            {
                if (_imagesEnumerator.MoveNext())
                {
                    try
                    {
                        byte[] data = File.ReadAllBytes(_imagesEnumerator.Current.FullName);
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
            DirectoryInfo[] songsDirs = _songsDir.GetDirectories();
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
                    return new FileInfo[] { songFile };
                });
            }).GetEnumerator();
        }

        #endregion

    }

    public class OsuImagesProvider : IImagesProvider
    {
        private readonly string[] _imgExtensions = { ".jpg", ".png", ".jpeg" };
        private DirectoryInfo _songsDir;
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
            foreach (byte[] data in GetBinaryIter(query))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(data);
                image.EndInit();
                yield return image;
            }
        }

        public IEnumerable<byte[]> GetBinaryIter(string query)
        {
            foreach (FileInfo file in GetFilesIter(query))
                yield return File.ReadAllBytes(file.FullName);
        }

        public IEnumerable<string> GetPathsIter(string query)
        {
            foreach (FileInfo file in GetFilesIter(query))
                yield return file.FullName;
        }

        public IEnumerable<FileInfo> GetFilesIter(string query)
        {
            DirectoryInfo[] songsDirs = _songsDir.GetDirectories();
            var items = songsDirs.Select(songDir => new
            {
                SongDir = songDir,
                Coef = StringEx.CalcSimilarityCoef(query, songDir.Name)
            }).ToArray();
            Array.Sort(items, (a, b) => b.Coef.CompareTo(a.Coef));

            foreach (var item in items)
            {
                foreach (FileInfo file in item.SongDir.GetFiles())
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
