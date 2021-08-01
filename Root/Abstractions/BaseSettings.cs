using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Root.Abstractions
{
    public abstract class BaseSettings : ISettings
    {
        public bool IsLoaded { get; private set; } = false;
        
        private readonly string _filePath;
        
        protected BaseSettings(string filePath)
        {
            _filePath = filePath;
        }
        
        public void Load()
        {
            try
            {
                var rawJson = File.ReadAllText(_filePath);
                var rootObj = JsonConvert.DeserializeObject<JToken>(rawJson);

                if (rootObj is null)
                {
                    IsLoaded = false;
                    return;
                }
                
                PropertiesFromJObject(rootObj);
                
                IsLoaded = true;
            }
            catch (Exception e)
            {
                IsLoaded = false;
                Console.WriteLine($"Error on load settings: {e.Message}");
            }
        }

        protected abstract void PropertiesFromJObject(JToken rootObj);
        
        public bool Save()
        {
            try
            {
                File.WriteAllText(_filePath, PropertiesToJObject()
                    .ToString(Formatting.Indented));
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