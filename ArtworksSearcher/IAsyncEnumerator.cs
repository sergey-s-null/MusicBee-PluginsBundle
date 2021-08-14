using System.Threading.Tasks;

namespace ArtworksSearcher
{
    public interface IAsyncEnumerator<out T>
    {
        T Current { get; }
        Task<bool> MoveNext();
    }
}
