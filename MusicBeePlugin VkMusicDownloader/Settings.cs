using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin
{
    public class Settings
    {
        private string _downloadDirectory = "";
        public string DownloadDirectory {
            get => _downloadDirectory;
            set
            {
                if (value is null)
                    return;
                _downloadDirectory = value;
            }
        }

        private string _ownerId = "";
        public string OwnerId
        {
            get => _ownerId;
            set
            {
                if (value is null)
                    return;
                _ownerId = value;
            }
        }

        private string _filePath;

        public Settings(string filePath)
        {
            _filePath = filePath;
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

            DownloadDirectory = rootObj.Value<string>(nameof(DownloadDirectory));
            OwnerId = rootObj.Value<string>(nameof(OwnerId));
            
            return true;
        }

        public bool Save()
        {
            JObject rootObj = new JObject();
            rootObj[nameof(DownloadDirectory)] = DownloadDirectory;
            rootObj[nameof(OwnerId)] = OwnerId;

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
