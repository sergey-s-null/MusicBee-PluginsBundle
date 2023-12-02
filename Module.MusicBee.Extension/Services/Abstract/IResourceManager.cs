namespace Module.MusicBee.Extension.Services.Abstract;

[Obsolete("мусор какой-то")]
public interface IResourceManager
{
    void CreateRootIfNeeded();
    void DeleteRoot();

    Stream? OpenRead(string resourceRelativePath);
    Stream OpenWrite(string resourceRelativePath);
}