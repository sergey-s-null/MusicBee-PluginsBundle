using VkNet.Model.Attachments;

namespace Module.VkAudioDownloader.Services.Abstract;

public interface IVkAudiosService
{
    IAsyncEnumerable<Audio> GetVkAudiosNotContainingInLibraryAsync();

    IAsyncEnumerable<Audio> GetVkAudiosContainingInIncomingAsync();
}