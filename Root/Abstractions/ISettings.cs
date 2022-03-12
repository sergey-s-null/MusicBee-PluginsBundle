namespace Root.Abstractions
{
    public interface ISettings
    {
        bool IsLoaded { get; }
        bool Load();
        bool Save();
        void Reset();
    }
}