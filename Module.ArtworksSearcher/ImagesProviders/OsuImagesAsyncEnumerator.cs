using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Ex;
using Root.Abstractions;

namespace Module.ArtworksSearcher.ImagesProviders
{
    internal class OsuImagesAsyncEnumerator : IAsyncEnumerator<BitmapImage>
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

}