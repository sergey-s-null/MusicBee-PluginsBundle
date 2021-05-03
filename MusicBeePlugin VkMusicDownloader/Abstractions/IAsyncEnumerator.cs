using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader.Abstractions
{
    public interface IAsyncEnumerator<out T>
    {
        T Current { get; }
        Task<bool> MoveNextAsync();
    }
}
