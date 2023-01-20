using Module.MBLibraryApiExtension.Entities.Abstract;

namespace Module.MBLibraryApiExtension.Factories.Abstract {}

public interface IMusicFileFactory
{
	IMusicFile Create(string filePath);
}