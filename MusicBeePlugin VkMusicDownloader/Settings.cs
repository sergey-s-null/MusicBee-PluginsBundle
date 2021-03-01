using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader
{
    public class Settings
    {
        private string _downloadDirectory = "";
        public string DownloadDirTemplate {
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

        private string _fileNameTemplate;
        public string FileNameTemplate
        {
            get => _fileNameTemplate;
            set
            {
                if (value is null)
                    return;
                _fileNameTemplate = value;
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

            DownloadDirTemplate = rootObj.Value<string>(nameof(DownloadDirTemplate));
            OwnerId = rootObj.Value<string>(nameof(OwnerId));
            FileNameTemplate = rootObj.Value<string>(nameof(FileNameTemplate));
            
            return true;
        }

        public bool Save()
        {
            JObject rootObj = new JObject();
            rootObj[nameof(DownloadDirTemplate)] = DownloadDirTemplate;
            rootObj[nameof(OwnerId)] = OwnerId;
            rootObj[nameof(FileNameTemplate)] = FileNameTemplate;

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
