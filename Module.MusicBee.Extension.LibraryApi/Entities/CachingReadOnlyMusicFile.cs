using System.Collections.Generic;
using Module.MusicBee.Autogen.Services.Abstract;
using Module.MusicBee.Enums;
using Module.MusicBee.Extension.LibraryApi.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryApi.Entities;

public class CachingReadOnlyMusicFile : IReadOnlyMusicFile
{
    public string Path { get; }

    public string Artist => GetTagValue(MetaDataType.Artist);
    public string TrackTitle => GetTagValue(MetaDataType.TrackTitle);

    private readonly IMusicBeeApi _musicBeeApi;

    private readonly IDictionary<MetaDataType, string> _cachedTagValues;

    public CachingReadOnlyMusicFile(
        string path,
        IMusicBeeApi musicBeeApi)
    {
        _musicBeeApi = musicBeeApi;

        _cachedTagValues = new Dictionary<MetaDataType, string>();

        Path = path;
    }

    public string GetTagValue(MetaDataType metaDataType)
    {
        if (_cachedTagValues.TryGetValue(metaDataType, out var value))
        {
            return value;
        }

        value = _musicBeeApi.Library_GetFileTag(Path, metaDataType);
        _cachedTagValues[metaDataType] = value;
        return value;
    }
}