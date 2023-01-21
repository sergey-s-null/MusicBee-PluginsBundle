using Root.MusicBeeApi;

namespace Module.MusicBee.Extension.LibraryApi.Entities.Abstract;

public interface IReadOnlyMusicFile
{
	string Path { get; }

	string Artist { get; }
	string TrackTitle { get; }

	string GetTagValue(MetaDataType metaDataType);
}