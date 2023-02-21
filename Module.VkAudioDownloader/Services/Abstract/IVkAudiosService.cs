using VkNet.Model.Attachments;

namespace Module.VkAudioDownloader.Services.Abstract;

public interface IVkAudiosService
{
    Task<IReadOnlyList<Audio>> GetVkAudiosNotContainingInLibraryAsync();

    Task<IReadOnlyList<Audio>> GetVkAudiosContainingInIncomingAsync();
}