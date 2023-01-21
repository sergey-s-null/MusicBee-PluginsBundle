using System.Collections.Generic;
using Module.MusicBee.Extension.LibraryApi.Entities.Abstract;
using Module.MusicBee.Extension.LibraryApi.Exceptions;

namespace Module.MusicBee.Extension.LibraryApi.Services.Abstract;

public interface IMusicBeeLibrary
{
    /// <exception cref="FilesRetrievingException"></exception>
    IReadOnlyList<IReadOnlyMusicFile> GetMusicFiles();

    /// <exception cref="FilesRetrievingException"></exception>
    IReadOnlyList<IMusicFile> GetEditableMusicFiles();
}