using System.Drawing;
using Module.MusicSourcesStorage.Core;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class ImageService : IImageService
{
    private readonly IModuleConfiguration _configuration;

    public ImageService(IModuleConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Image ResizeForStorage(Image image)
    {
        var targetSize = GetTargetSize(image.Size, _configuration.CoverSize);
        var target = new Bitmap(image, targetSize);
        return target;
    }

    private static Size GetTargetSize(Size sourceSize, int targetSize)
    {
        return GetTargetSize(sourceSize, new Size(targetSize, targetSize));
    }

    private static Size GetTargetSize(Size sourceSize, Size targetBound)
    {
        var sourceRatio = (double)sourceSize.Width / sourceSize.Height;
        var targetRatio = (double)targetBound.Width / targetBound.Height;

        return targetRatio > sourceRatio
            ? targetBound with { Width = (int)(sourceRatio * targetBound.Height) }
            : targetBound with { Height = (int)(targetBound.Width / sourceRatio) };
    }
}