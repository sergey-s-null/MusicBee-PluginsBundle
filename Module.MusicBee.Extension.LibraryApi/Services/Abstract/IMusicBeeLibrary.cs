using System.Collections.Generic;
using Module.MusicBee.Extension.LibraryApi.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryApi.Services.Abstract;

public interface IMusicBeeLibrary
{
    IReadOnlyList<IReadOnlyMusicFile> GetMusicFiles();

    IReadOnlyList<IMusicFile> GetEditableMusicFiles();
}