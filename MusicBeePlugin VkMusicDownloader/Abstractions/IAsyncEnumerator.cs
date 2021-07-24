using System.Threading.Tasks;

namespace VkMusicDownloader.Abstractions
{
    public interface IAsyncEnumerator<out T>
    {
        T Current { get; }
        Task<bool> MoveNextAsync();
    }
}
