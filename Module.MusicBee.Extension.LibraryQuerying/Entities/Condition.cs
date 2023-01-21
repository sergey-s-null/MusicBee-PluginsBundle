using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Extensions;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

public static class Condition
{
    public static readonly BaseCondition Empty = Field.None.StartsWith(string.Empty);

    public static readonly BaseCondition HasLyrics = Field.HasLyrics.IsTrue();
    public static readonly BaseCondition HasNotLyrics = Field.HasLyrics.IsFalse();

    public static readonly BaseCondition AlbumPlayed = Field.AlbumPlayed.IsTrue();
    public static readonly BaseCondition AlbumNotPlayed = Field.AlbumPlayed.IsFalse();
    
    public static readonly BaseCondition AlbumComplete = Field.AlbumComplete.IsTrue();
    public static readonly BaseCondition AlbumNotComplete = Field.AlbumComplete.IsFalse();
}