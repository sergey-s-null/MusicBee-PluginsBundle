using System.Threading.Tasks;

namespace Module.VkMusicDownloader.Abstractions
{
    public interface IAsyncEnumerator<out T>
    {
        T Current { get; }
        Task<bool> MoveNextAsync();
    }
}
