using System.Drawing;
using System.Drawing.Imaging;

namespace Module.MusicSourcesStorage.Logic.Helpers;

public static class ImageHelper
{
    public static byte[] ToBytes(Image image)
    {
        return ToBytes(image, ImageFormat.Png);
    }

    public static byte[] ToBytes(Image image, ImageFormat format)
    {
        using var memoryStream = new MemoryStream();
        image.Save(memoryStream, format);
        return memoryStream.ToArray();
    }
}