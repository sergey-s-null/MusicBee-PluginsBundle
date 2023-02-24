namespace Module.AudioSourcesComparer.DataClasses;

public sealed record AudiosDifference(IReadOnlyCollection<VkAudio> VkOnly, IReadOnlyCollection<MBAudio> MBOnly);