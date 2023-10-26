using System.ComponentModel.DataAnnotations;

namespace Module.MusicSourcesStorage.Database.Models;

public sealed class Cover : File
{
    [Required] public byte[] Data { get; set; } = Array.Empty<byte>();
}