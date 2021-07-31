namespace Root.Abstractions
{
    public interface ISettings
    {
        bool Save();
        void Reset();
    }
}