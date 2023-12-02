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

    /// <summary>
    /// "Safely" means that it does not store file descriptor - just read bytes from file and use it.
    /// </summary>
    public static Image FromFileSafely(string filePath)
    {
        return FromBytes(File.ReadAllBytes(filePath));
    }

    public static Image FromBytes(byte[] bytes)
    {
        using var stream = new MemoryStream(bytes);
        return new Bitmap(stream);
    }
}