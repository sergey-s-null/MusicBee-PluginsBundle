using System.ComponentModel.DataAnnotations;

namespace Module.MusicSourcesStorage.Database.Models;

public sealed class TorrentSourceModel : MusicSourceModel
{
    [Required] public byte[] TorrentFile { get; set; } = Array.Empty<byte>();
}