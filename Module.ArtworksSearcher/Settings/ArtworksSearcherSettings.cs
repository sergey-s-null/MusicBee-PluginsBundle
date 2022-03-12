using Newtonsoft.Json.Linq;
using Root;
using Root.Abstractions;

namespace Module.ArtworksSearcher.Settings
{
    public class ArtworksSearcherSettings : BaseSettings, IArtworksSearcherSettings
    {
        public string GoogleCX { get; set; } = "";
        public string GoogleKey { get; set; } = "";
        
        public int MaxParallelDownloadsCount => 10;// TODO from config

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
        
        public ArtworksSearcherSettings(IResourceManager resourceManager) 
            : base(ResourcePaths.ArtworksSearcherSettingsPath, true, resourceManager)
        {
        }
        
        protected override void PropertiesFromJObject(JToken rootObj)
        {
            GoogleCX = rootObj.Value<string>(nameof(GoogleCX)) ?? "";
            GoogleKey = rootObj.Value<string>(nameof(GoogleKey)) ?? "";
            ParallelDownloadsCount = rootObj.Value<int>(nameof(ParallelDownloadsCount));
            OsuSongsDir = rootObj.Value<string>(nameof(OsuSongsDir)) ?? "";
            MinOsuImageByteSize = rootObj.Value<long>(nameof(MinOsuImageByteSize));
        }
        
        protected override JObject PropertiesToJObject()
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