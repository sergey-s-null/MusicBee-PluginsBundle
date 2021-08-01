namespace Root.Abstractions
{
    public interface ISettings
    {
        bool IsLoaded { get; }
        void Load();
        bool Save();
        void Reset();
    }
}