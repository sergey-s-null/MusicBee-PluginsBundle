using System.Drawing;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IImageService
{
    Image ResizeForStorage(Image image);
}