using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Exceptions;
using Root.Helpers;
using Root.Services.Abstract;
using Root.Settings;

namespace Module.ArtworksSearcher.Settings
{
    public class ArtworksSearcherSettings : BaseSettings, IArtworksSearcherSettings
    {
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

        public ArtworksSearcherSettings(ISettingsJsonLoader settingsJsonLoader)
            : base(ResourcesHelper.ArtworksSearcherSettingsPath, settingsJsonLoader)
        {
        }

        protected override void SetSettingsFromJObject(JObject rootObj)
        {
            try
            {
                GoogleCX = rootObj.Value<string>(nameof(GoogleCX)) ?? "";
                GoogleKey = rootObj.Value<string>(nameof(GoogleKey)) ?? "";
                ParallelDownloadsCount = rootObj.Value<int>(nameof(ParallelDownloadsCount));
                OsuSongsDir = rootObj.Value<string>(nameof(OsuSongsDir)) ?? "";
                MinOsuImageByteSize = rootObj.Value<long>(nameof(MinOsuImageByteSize));
            }
            catch (JsonException e)
            {
                throw new SettingsLoadException("Error on set settings from json object.", e);
            }
        }

        protected override JObject GetSettingsAsJObject()
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