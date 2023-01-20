using Module.MBLibraryApiExtension.Entities.Abstract;

namespace Module.MBLibraryApiExtension.Factories.Abstract
{
	public interface IReadOnlyMusicFileFactory
	{
		IReadOnlyMusicFile Create(string filePath);
	}
}