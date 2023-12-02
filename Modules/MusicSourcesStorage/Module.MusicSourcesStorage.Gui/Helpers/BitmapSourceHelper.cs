using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Module.MusicSourcesStorage.Gui.Helpers;

public static class BitmapSourceHelper
{
    public static BitmapSource Create(byte[] binaryImage)
    {
        using var stream = new MemoryStream(binaryImage);

        return CreateFromStream(stream);
    }

    public static BitmapSource Create(Image image)
    {
        using var stream = new MemoryStream();
        image.Save(stream, ImageFormat.Png);

        return CreateFromStream(stream);
    }

    private static BitmapSource CreateFromStream(Stream stream)
    {
        var bitmapSource = new BitmapImage();
        bitmapSource.BeginInit();
        bitmapSource.CacheOption = BitmapCacheOption.OnLoad;
        bitmapSource.StreamSource = stream;
        bitmapSource.EndInit();

        return bitmapSource;
    }
}