using System.Collections.Generic;
using Module.MBLibraryApiExtension.Entities.Abstract;

namespace Module.MBLibraryApiExtension.Services.Abstract;

public interface IMusicBeeLibrary
{
	IReadOnlyList<IReadOnlyMusicFile> GetMusicFiles();

	IReadOnlyList<IMusicFile> GetEditableMusicFiles();
}