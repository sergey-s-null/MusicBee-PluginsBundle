using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader
{
    public interface IAsyncEnumerator<out T>
    {
        T Current { get; }
        Task<bool> MoveNextAsync();
    }
}
