namespace Module.Vk.Settings
{
    public interface IVkSettings
    {
        string AccessToken { get; set; }

        // todo with Result
        void Load();
        void Save();
    }
}