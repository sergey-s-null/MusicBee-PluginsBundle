using Newtonsoft.Json.Linq;
using Root.Exceptions;

namespace Root.Services.Abstract
{
    public interface ISettingsJsonLoader
    {
        /// <exception cref="SettingsIOException">IO or Json error on setting loading.</exception>
        JObject Load(string path);

        /// <exception cref="SettingsIOException">IO or Json error on setting saving.</exception>
        void Save(string path, JObject settings);
    }
}