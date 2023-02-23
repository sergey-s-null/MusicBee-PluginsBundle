using Module.VkAudioDownloader.Entities;

namespace Module.VkAudioDownloader.Services.Abstract;

public interface IVkAudiosService
{
    /// <summary>
    /// Returns audios:<br/>
    /// 1. presented only in Vk;<br/>
    /// 2. presented in Vk and in Incoming.
    /// </summary>
    IAsyncEnumerable<VkAudioModel> GetVkAudiosToDisplay();

    /// <summary>
    /// Returns audios:<br/>
    /// 1. presented only in Vk;<br/>
    /// 2. presented in Vk and in Incoming.<br/>
    /// Takes audios until meets audio presented both in Vk and Library.
    /// </summary>
    IAsyncEnumerable<VkAudioModel> GetFirstVkAudiosToDisplay();
}