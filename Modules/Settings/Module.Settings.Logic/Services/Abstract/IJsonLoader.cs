using Module.Settings.Logic.Exceptions;
using Newtonsoft.Json.Linq;

namespace Module.Settings.Logic.Services.Abstract;

public interface IJsonLoader
{
    /// <exception cref="SettingsIOException">IO or Json error on setting loading.</exception>
    JObject Load(string filePath);

    /// <exception cref="SettingsIOException">IO or Json error on setting saving.</exception>
    void Save(string filePath, JObject json);
}