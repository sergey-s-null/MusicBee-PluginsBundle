using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VkMusicDownloader.Settings
{
    public abstract class BaseJsonFilePropertiesManager
    {
        public bool IsLoaded { get; private set; }
        
        protected abstract IEnumerable<PropertyInfo> PropertyInfos { get; }

        private readonly string _filePath; 
        
        protected BaseJsonFilePropertiesManager(string filePath)
        {
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            IsLoaded = false;
            
            try
            {
                var rawJson = File.ReadAllText(_filePath);
                var rootObj = JsonConvert.DeserializeObject<JObject>(rawJson);

                foreach (var propInfo in PropertyInfos)
                {
                    var type = propInfo.PropertyType;
                    var newValue = rootObj.GetValue(propInfo.Name)?.ToObject(type);
                    if (newValue is not null)
                    {
                        propInfo.SetValue(this, newValue);
                    }
                }
                
                IsLoaded = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error on load settings: {e.Message}");
            }
        }

        public bool Save()
        {
            var rootObj = new JObject();
            
            foreach (var propInfo in PropertyInfos)
            {
                rootObj[propInfo.Name] = JToken.FromObject(propInfo.GetValue(this));
            }

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