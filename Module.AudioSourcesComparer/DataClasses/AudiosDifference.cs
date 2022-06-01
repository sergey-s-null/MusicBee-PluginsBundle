using System.Collections.Generic;

namespace Module.AudioSourcesComparer.DataClasses
{
    public record AudiosDifference(IReadOnlyCollection<VkAudio> VkOnly, IReadOnlyCollection<MBAudio> MBOnly);
}