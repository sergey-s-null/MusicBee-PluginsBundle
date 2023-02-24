using System.IO;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Settings;
using Module.Core.Helpers;
using StringHelper = Module.ArtworksSearcher.Helpers.StringHelper;

namespace Module.ArtworksSearcher.ImagesProviders;

public sealed class OsuImagesProvider : IAsyncEnumerable<BitmapImage>
{
    private readonly string[] _imgExtensions = { ".jpg", ".png", ".jpeg" };
        
    private readonly string _query;
    private readonly DirectoryInfo _songsDir;
    private readonly long _minSize;

    public OsuImagesProvider(
        string query,
        // DI
        IArtworksSearcherSettings settings)
    {
        _query = query;
            
        _songsDir = new DirectoryInfo(settings.OsuSongsDir);
        _minSize = settings.MinOsuImageByteSize;
    }

    public IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(CancellationToken cancellationToken)
    {
        return GetImagesAsyncEnumerator(cancellationToken);
    }
        
    private IAsyncEnumerator<BitmapImage> GetImagesAsyncEnumerator(CancellationToken cancellationToken)
    {
        var songsDirs = _songsDir.GetDirectories();

        var orderedByRelevance = songsDirs
            .ToAsyncEnumerable()
            .Select(songDir => new
            {
                SongDir = songDir,
                Coef = StringHelper.CalcSimilarityCoefficient(_query, songDir.Name)
            })
            .OrderByDescending(x => x.Coef)
            .Select(x => x.SongDir);

        return orderedByRelevance
            .SelectMany(item => item.GetFiles().ToAsyncEnumerable())
            .Where(fInfo => fInfo.Length >= _minSize)
            .Where(fInfo => _imgExtensions.Contains(fInfo.Extension.ToLower()))
            .Select(ToBitmapImageOrNull)
            .WhereNotNull()
            .GetAsyncEnumerator(cancellationToken);
    }

    private static BitmapImage? ToBitmapImageOrNull(FileInfo fileInfo)
    {
        try
        {
            var data = File.ReadAllBytes(fileInfo.FullName);
                
            var image = new BitmapImage();
                
            image.BeginInit();
            image.StreamSource = new MemoryStream(data);
            image.EndInit();

            return image;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}