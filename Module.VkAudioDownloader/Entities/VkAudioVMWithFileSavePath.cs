using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.Entities;

internal sealed record VkAudioVMWithFileSavePath(IVkAudioVM VkAudio, string FilePath);