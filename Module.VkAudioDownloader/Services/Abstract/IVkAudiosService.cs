using Module.VkAudioDownloader.Entities;

namespace Module.VkAudioDownloader.Services.Abstract;

public interface IVkAudiosService
{
    /// <summary>
    /// Returns audios
    /// 1. presented only in Vk
    /// 2. presented in Vk and in Incoming
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<VkAudioModel> GetVkAudiosToDisplay();
}