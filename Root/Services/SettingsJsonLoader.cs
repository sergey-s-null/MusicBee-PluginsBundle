using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Exceptions;
using Root.Services.Abstract;

namespace Root.Services
{
    public sealed class SettingsJsonLoader : ISettingsJsonLoader
    {
        private readonly IResourceManager _resourceManager;

        public SettingsJsonLoader(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public JObject Load(string path)
        {
            var rawJson = ReadRawJson(path);

            JObject? rootObj;
            try
            {
                rootObj = JsonConvert.DeserializeObject<JObject>(rawJson);
            }
            catch (JsonException e)
            {
                throw new SettingsIOException($"Error on deserialize json object from file \"{path}\".", e);
            }

            if (rootObj is null)
            {
                throw new SettingsIOException($"Could not deserialize content of file with path \"{path}\".");
            }

            return rootObj;
        }

        private string ReadRawJson(string path)
        {
            try
            {
                using var stream = _resourceManager.OpenRead(path);
                if (stream is null)
                {
                    throw new SettingsIOException($"Could not open read stream for path \"{path}\".");
                }

                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch (IOException e)
            {
                throw new SettingsIOException($"IO error on reading from \"{path}\".", e);
            }
        }

        public void Save(string path, JObject settings)
        {
            try
            {
                using var stream = _resourceManager.OpenWrite(path);
                using var writer = new StreamWriter(stream);
                writer.Write(settings.ToString(Formatting.Indented));
            }
            catch (IOException e)
            {
                throw new SettingsIOException($"IO error on write settings to file with path \"{path}\".", e);
            }
            catch (JsonException e)
            {
                throw new SettingsIOException($"Json serialization error on save settings.", e);
            }
        }
    }
}