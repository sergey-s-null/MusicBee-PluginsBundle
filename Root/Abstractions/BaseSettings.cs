using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Root.Abstractions
{
    public abstract class BaseSettings : ISettings
    {
        public bool IsLoaded { get; private set; }
        
        private readonly IResourceManager _resourceManager;
        
        private readonly string _path;
        
        protected BaseSettings(
            string path, 
            bool load,
            IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            
            _path = path;

            if (load)
            {
                Load();
            }
        }
        
        public bool Load()
        {
            var rawJson = ReadRawJson();
            if (rawJson is null)
            {
                IsLoaded = false;
                return false;
            }
            
            try
            {
                var rootObj = JsonConvert.DeserializeObject<JToken>(rawJson);

                if (rootObj is null)
                {
                    IsLoaded = false;
                    return false;
                }
                
                PropertiesFromJObject(rootObj);
                
                IsLoaded = true;
                return true;
            }
            catch (JsonException e)
            {
                IsLoaded = false;
                Console.WriteLine($"Error on load settings: {e.Message}");
                return false;
            }
        }

        private string? ReadRawJson()
        {
            using var stream = _resourceManager.OpenRead(_path);
            if (stream is null)
            {
                return null;
            }
            
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        protected abstract void PropertiesFromJObject(JToken rootObj);
        
        public bool Save()
        {
            try
            {
                using var stream = _resourceManager.OpenWrite(_path);
                using var writer = new StreamWriter(stream);
                writer.Write(PropertiesToJObject().ToString(Formatting.Indented));
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        protected abstract JObject PropertiesToJObject();

        public void Reset()
        {
            // TODO возможно, стоит не читать файл, а восстанавливать с предыдущего чтения
            Load();
        }
    }
}