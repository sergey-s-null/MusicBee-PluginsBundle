using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VkMusicDownloader.Settings
{
    public class MusicDownloaderSettings : BaseJsonFilePropertiesManager, IMusicDownloaderSettings
    {

        #region Settings Props With Reflection

        private static readonly IReadOnlyCollection<string> PropsNamesList = new List<string>()
        {
            nameof(DownloadDirTemplate),
            nameof(FileNameTemplate),
            nameof(AccessToken)
        };

        private static readonly IReadOnlyCollection<PropertyInfo> PropsInfo = typeof(MusicDownloaderSettings)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => PropsNamesList.Contains(p.Name))
            .ToList()
            .AsReadOnly();

        protected override IEnumerable<PropertyInfo> PropertyInfos => PropsInfo;

        public string DownloadDirTemplate { get; set; } = "";

        public string FileNameTemplate { get; set; } = "";

        public string AccessToken { get; set; } = "";

        #endregion

        public MusicDownloaderSettings(string filePath) : base(filePath)
        { }
    }
}
