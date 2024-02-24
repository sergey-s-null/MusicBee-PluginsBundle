using Module.Settings.Logic.Exceptions;
using Module.Settings.Logic.Services.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Module.Settings.Logic.Services;

public sealed class JsonLoader : IJsonLoader
{
    public JObject Load(string filePath)
    {
        var rawJson = ReadRawJson(filePath);

        JObject? rootObj;
        try
        {
            rootObj = JsonConvert.DeserializeObject<JObject>(rawJson);
        }
        catch (JsonException e)
        {
            throw new SettingsIOException($"Error on deserialize json object from file \"{filePath}\".", e);
        }

        if (rootObj is null)
        {
            throw new SettingsIOException($"Could not deserialize content of file with path \"{filePath}\".");
        }

        return rootObj;
    }

    private static string ReadRawJson(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new SettingsIOException($"File \"{filePath}\" does not exists.");
        }

        try
        {
            using var stream = File.OpenRead(filePath);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        catch (IOException e)
        {
            throw new SettingsIOException($"IO error on reading from \"{filePath}\".", e);
        }
    }

    public void Save(string filePath, JObject json)
    {
        try
        {
            using var stream = File.OpenWrite(filePath);
            using var writer = new StreamWriter(stream);
            writer.Write(json.ToString(Formatting.Indented));
        }
        catch (IOException e)
        {
            throw new SettingsIOException($"IO error on write settings to file with path \"{filePath}\".", e);
        }
        catch (JsonException e)
        {
            throw new SettingsIOException($"Json serialization error on save settings.", e);
        }
    }
}