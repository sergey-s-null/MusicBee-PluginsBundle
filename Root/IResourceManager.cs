using System.IO;

namespace Root
{
    public interface IResourceManager
    {
        void CreateRootIfNeeded();
        void DeleteRoot();
        
        Stream? OpenRead(string resourceRelativePath);
        Stream OpenWrite(string resourceRelativePath);
    }
}