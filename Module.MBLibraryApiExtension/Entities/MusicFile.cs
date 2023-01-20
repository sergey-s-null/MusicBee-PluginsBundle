using System.Collections.Generic;
using Module.MBLibraryApiExtension.Entities.Abstract;
using Module.MBLibraryApiExtension.Factories.Abstract;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;

namespace Module.MBLibraryApiExtension.Entities;

public class MusicFile : IMusicFile
{
	public string Path { get; }

	public string Artist
	{
		get => GetTagValue(MetaDataType.Artist);
		set => SetTagValue(MetaDataType.Artist, value);
	}

	public string TrackTitle
	{
		get => GetTagValue(MetaDataType.TrackTitle);
		set => SetTagValue(MetaDataType.TrackTitle, value);
	}

	private readonly IMusicBeeApi _musicBeeApi;
	private readonly IReadOnlyMusicFileFactory _readOnlyMusicFileFactory;

	private readonly IDictionary<MetaDataType, string> _changedTagValues;
	private IReadOnlyMusicFile _musicFileSnapshot;

	public MusicFile(
		string filePath,
		IMusicBeeApi musicBeeApi,
		IReadOnlyMusicFileFactory readOnlyMusicFileFactory)
	{
		_musicBeeApi = musicBeeApi;
		_readOnlyMusicFileFactory = readOnlyMusicFileFactory;

		Path = filePath;

		_changedTagValues = new Dictionary<MetaDataType, string>();
		_musicFileSnapshot = _readOnlyMusicFileFactory.Create(Path);
	}

	public string GetTagValue(MetaDataType metaDataType)
	{
		return _changedTagValues.TryGetValue(metaDataType, out var value)
			? value
			: _musicFileSnapshot.GetTagValue(metaDataType);
	}

	public void SetTagValue(MetaDataType metaDataType, string value)
	{
		if (value == _musicFileSnapshot.GetTagValue(metaDataType))
		{
			_changedTagValues.Remove(metaDataType);
		}
		else
		{
			_changedTagValues[metaDataType] = value;
		}
	}

	public void Restore()
	{
		_changedTagValues.Clear();
	}

	public void Save()
	{
		foreach (var kvp in _changedTagValues)
		{
			var metaDataType = kvp.Key;
			var value = kvp.Value;
			_musicBeeApi.Library_SetFileTag(Path, metaDataType, value);
		}

		_musicBeeApi.Library_CommitTagsToFile(Path);

		_musicFileSnapshot = _readOnlyMusicFileFactory.Create(Path);
	}
}