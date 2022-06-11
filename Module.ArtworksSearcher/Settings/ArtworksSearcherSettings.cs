using Newtonsoft.Json.Linq;
using Root.Services.Abstract;

namespace Module.ArtworksSearcher.Settings
{
    public class ArtworksSearcherSettings : IArtworksSearcherSettings
    {
        // todo from config
        private const string ArtworksSearcherSettingsPath = "ArtworksSearcher/settings.json";

        public string GoogleCX { get; set; } = "";
        public string GoogleKey { get; set; } = "";

        public int MaxParallelDownloadsCount => 10; // TODO from config

        private int _parallelDownloadsCount;

        public int ParallelDownloadsCount
        {
            get => _parallelDownloadsCount;
            set
            {
                if (value < 1)
                {
                    _parallelDownloadsCount = 1;
                }
                else if (value > MaxParallelDownloadsCount)
                {
                    _parallelDownloadsCount = MaxParallelDownloadsCount;
                }
                else
                {
                    _parallelDownloadsCount = value;
                }
            }
        }

        public string OsuSongsDir { get; set; } = "";
        public long MinOsuImageByteSize { get; set; }

        private readonly ISettingsJsonLoader _settingsJsonLoader;

        public ArtworksSearcherSettings(ISettingsJsonLoader settingsJsonLoader)
        {
            _settingsJsonLoader = settingsJsonLoader;
        }

        public void Load()
        {
            // todo handle exceptions
            var jSettings = _settingsJsonLoader.Load(ArtworksSearcherSettingsPath);
            SetSettingsFromJObject(jSettings);
        }

        public void Save()
        {
            // todo handle exceptions
            var jSettings = GetSettingsAsJObject();
            _settingsJsonLoader.Save(ArtworksSearcherSettingsPath, jSettings);
        }

        private void SetSettingsFromJObject(JToken rootObj)
        {
            GoogleCX = rootObj.Value<string>(nameof(GoogleCX)) ?? "";
            GoogleKey = rootObj.Value<string>(nameof(GoogleKey)) ?? "";
            ParallelDownloadsCount = rootObj.Value<int>(nameof(ParallelDownloadsCount));
            OsuSongsDir = rootObj.Value<string>(nameof(OsuSongsDir)) ?? "";
            MinOsuImageByteSize = rootObj.Value<long>(nameof(MinOsuImageByteSize));
        }

        private JObject GetSettingsAsJObject()
        {
            return new JObject
            {
                [nameof(GoogleCX)] = GoogleCX,
                [nameof(GoogleKey)] = GoogleKey,
                [nameof(ParallelDownloadsCount)] = ParallelDownloadsCount,
                [nameof(OsuSongsDir)] = OsuSongsDir,
                [nameof(MinOsuImageByteSize)] = MinOsuImageByteSize
            };
        }
    }
}