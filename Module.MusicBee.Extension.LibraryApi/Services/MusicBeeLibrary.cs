using Mead.MusicBee.Api.Services.Abstract;
using Module.MusicBee.Extension.LibraryApi.Entities.Abstract;
using Module.MusicBee.Extension.LibraryApi.Exceptions;
using Module.MusicBee.Extension.LibraryApi.Factories.Abstract;
using Module.MusicBee.Extension.LibraryApi.Services.Abstract;

namespace Module.MusicBee.Extension.LibraryApi.Services;

public class MusicBeeLibrary : IMusicBeeLibrary
{
    private readonly IMusicBeeApi _musicBeeApi;
    private readonly ReadOnlyMusicFileFactory _readOnlyMusicFileFactory;
    private readonly MusicFileFactory _musicFileFactory;

    public MusicBeeLibrary(
        IMusicBeeApi musicBeeApi,
        ReadOnlyMusicFileFactory readOnlyMusicFileFactory,
        MusicFileFactory musicFileFactory)
    {
        _musicBeeApi = musicBeeApi;
        _readOnlyMusicFileFactory = readOnlyMusicFileFactory;
        _musicFileFactory = musicFileFactory;
    }

    public IReadOnlyList<IReadOnlyMusicFile> GetMusicFiles()
    {
        return GetFilePaths()
            .Select(x => _readOnlyMusicFileFactory(x))
            .ToList();
    }

    public IReadOnlyList<IMusicFile> GetEditableMusicFiles()
    {
        return GetFilePaths()
            .Select(x => _musicFileFactory(x))
            .ToList();
    }

    private IEnumerable<string> GetFilePaths()
    {
        if (!_musicBeeApi.Library_QueryFilesEx(string.Empty, out var files)
            || files is null)
        {
            throw new FilesRetrievingException("Could not get files path from MusicBee API.");
        }

        return files;
    }
}