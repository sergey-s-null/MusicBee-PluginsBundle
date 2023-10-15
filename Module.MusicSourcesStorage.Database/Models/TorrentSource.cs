using System.ComponentModel.DataAnnotations;

namespace Module.MusicSourcesStorage.Database.Models;

public sealed class TorrentSource : MusicSource
{
    [Required] public byte[] TorrentFile { get; set; } = null!;
}