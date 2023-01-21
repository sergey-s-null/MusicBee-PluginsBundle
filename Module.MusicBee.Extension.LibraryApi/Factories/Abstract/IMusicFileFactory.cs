using Module.MusicBee.Extension.LibraryApi.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryApi.Factories.Abstract {}

public interface IMusicFileFactory
{
	IMusicFile Create(string filePath);
}