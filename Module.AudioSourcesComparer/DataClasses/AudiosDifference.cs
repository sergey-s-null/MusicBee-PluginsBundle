using System.Collections.Generic;

namespace Module.AudioSourcesComparer.DataClasses
{
    public sealed record AudiosDifference(IReadOnlyCollection<VkAudio> VkOnly, IReadOnlyCollection<MBAudio> MBOnly);
}