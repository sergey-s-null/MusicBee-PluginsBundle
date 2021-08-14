using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworksSearcher
{
    public class Settings
    {
        #region SettingsFields

        private string _googleCX = "";
        public string GoogleCX
        {
            get => _googleCX;
            set
            {
                if (value is null)
                    return;
                _googleCX = value;
            }
        }

        private string _googleKey = "";
        public string GoogleKey
        {
            get => _googleKey;
            set
            {
                if (value is null)
                    return;
                _googleKey = value;
            }
        }

        public const int MaxParallelDownloadsCount = 10;

        private int _parallelDownloadsCount = 1;
        public int ParallelDownloadsCount
        {
            get => _parallelDownloadsCount;
            set
            {
                if (value < 1)
                    _parallelDownloadsCount = 1;
                else if (value > MaxParallelDownloadsCount)
                    _parallelDownloadsCount = MaxParallelDownloadsCount;
                else
                    _parallelDownloadsCount = value;
            }
        }

        private string _osuSongsDir = "";
        public string OsuSongsDir
        {
            get => _osuSongsDir;
            set
            {
                if (value is null)
                    return;
                _osuSongsDir = value;
            }
        }

        private long _minOsuImageByteSize = 0;
        public long MinOsuImageByteSize
        {
            get => _minOsuImageByteSize;
            set
            {
                if (value < 0)
                    _minOsuImageByteSize = 0;
                else
                    _minOsuImageByteSize = value;
            }
        }

        #endregion

        private string _filePath;

        public Settings(string settingsFilePath)
        {
            _filePath = settingsFilePath;
            Load();
        }

        public bool Load()
        {
            string content;
            try
            {
                content = File.ReadAllText(_filePath);
            }
            catch
            {
                return false;
            }

            JObject rootObj = JsonConvert.DeserializeObject(content) as JObject;
            if (rootObj is null)
                return false;

            GoogleCX = rootObj.Value<string>(nameof(GoogleCX));
            GoogleKey = rootObj.Value<string>(nameof(GoogleKey));
            ParallelDownloadsCount = rootObj.Value<int>(nameof(ParallelDownloadsCount));
            OsuSongsDir = rootObj.Value<string>(nameof(OsuSongsDir));
            MinOsuImageByteSize = rootObj.Value<long>(nameof(MinOsuImageByteSize));

            return true;
        }

        public bool Save()
        {
            JObject rootObj = new JObject();
            rootObj[nameof(GoogleCX)] = GoogleCX;
            rootObj[nameof(GoogleKey)] = GoogleKey;
            rootObj[nameof(ParallelDownloadsCount)] = ParallelDownloadsCount;
            rootObj[nameof(OsuSongsDir)] = OsuSongsDir;
            rootObj[nameof(MinOsuImageByteSize)] = MinOsuImageByteSize;

            try
            {
                File.WriteAllText(_filePath, rootObj.ToString(Formatting.Indented));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
