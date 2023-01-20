using Root.MusicBeeApi;

namespace Module.MBLibraryApiExtension.Entities.Abstract;

public interface IMusicFile
{
	string Path { get; }

	string Artist { get; set; }
	string TrackTitle { get; set; }

	string GetTagValue(MetaDataType metaDataType);
	void SetTagValue(MetaDataType metaDataType, string value);

	void Restore();
	void Save();
}