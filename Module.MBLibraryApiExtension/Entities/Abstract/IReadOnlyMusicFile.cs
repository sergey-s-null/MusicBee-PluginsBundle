using Root.MusicBeeApi;

namespace Module.MBLibraryApiExtension.Entities.Abstract;

public interface IReadOnlyMusicFile
{
	string Path { get; }

	string Artist { get; }
	string TrackTitle { get; }

	string GetTagValue(MetaDataType metaDataType);
}