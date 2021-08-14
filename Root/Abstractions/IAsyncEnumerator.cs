using System.Threading.Tasks;

namespace Root.Abstractions
{
    public interface IAsyncEnumerator<out T>
    {
        T Current { get; }
        Task<bool> MoveNextAsync();
    }
}
