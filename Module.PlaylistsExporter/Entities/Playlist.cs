using System.Text;

namespace Module.PlaylistsExporter.Entities;

public sealed record Playlist(string Path, IReadOnlyCollection<string> FilePaths)
{
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append($"{nameof(Playlist)} \"{Path}\":\n");
        foreach (var filePath in FilePaths)
        {
            builder.Append($"\t{filePath}\n");
        }
            
        return builder.ToString();
    }
}