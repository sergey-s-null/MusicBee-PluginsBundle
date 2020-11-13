using MusicBeePlugin.GUI;
using MusicBeePlugin_VkMusicDownloader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;


namespace MusicBeePlugin
{
    public partial class Plugin
    {
        // TODO encrypt cookies
        public static MusicBeeApiInterface MBApiInterface;
        public static Settings Settings;
        private static string _settingsDirName = "Laiser399_VkAudioDownloader";
        private static string _cookiesFileName = "cookies.json";
        private static string _settingsFileName = "settings.json";

        private PluginInfo about = new PluginInfo();
        private VkAudioApi _vkAudioApi;

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            ServicePointManager.DefaultConnectionLimit = 20;

            MBApiInterface = new MusicBeeApiInterface();
            MBApiInterface.Initialise(apiInterfacePtr);
            InitAbout();
            CreateSettingsDirectory();
            InitSettings();
            InitVkApi();

            MBApiInterface.MB_AddMenuItem("mnuTools/Laiser399: download vk audio",
                "Laiser399: download vk audio", (_, _) => OpenDownloadDialog());

            return about;
        }

        private void InitAbout()
        {
            about.PluginInfoVersion = PluginInfoVersion;
            about.Name = "Laiser399: VK audios downloader";
            about.Description = "Download audios from vk and set custom1, custom2 tags";
            about.Author = "Laiser399";
            about.TargetApplication = "";   //  the name of a Plugin Storage device or panel header for a dockable panel
            about.Type = PluginType.General;
            about.VersionMajor = 1;  // your plugin version
            about.VersionMinor = 0;
            about.Revision = 1;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = ReceiveNotificationFlags.StartupOnly;
            about.ConfigurationPanelHeight = 0;
        }

        private void InitVkApi()
        {
            string dataPath = MBApiInterface.Setting_GetPersistentStoragePath();
            _vkAudioApi = new VkAudioApi(Settings.OwnerId, Path.Combine(dataPath, _settingsDirName, _cookiesFileName));
        }

        private void CreateSettingsDirectory()
        {
            string dataPath = MBApiInterface.Setting_GetPersistentStoragePath();
            string settingsDirPath = Path.Combine(dataPath, _settingsDirName);
            if (!Directory.Exists(settingsDirPath))
                Directory.CreateDirectory(settingsDirPath);
        }

        private void InitSettings()
        {
            string dataPath = MBApiInterface.Setting_GetPersistentStoragePath();
            string settingsFilePath = Path.Combine(dataPath, _settingsDirName, _settingsFileName);
            Settings = new Settings(settingsFilePath);
        }

        private void OpenDownloadDialog()
        {
            if (!_vkAudioApi.IsAuthorized)
            {
                if (!_vkAudioApi.TryAuth(TryInputAuthData, TryInputCode))
                {
                    System.Windows.MessageBox.Show("Auth error.");
                    return;
                }
            }

            var downloadDialog = new MainWindow(_vkAudioApi);
            downloadDialog.ShowDialog();
        }

        private void OpenSettingsDialog()
        {
            var settingsDialog = new SettingsDialog();
            settingsDialog.ShowDialog();
        }

        private bool TryInputAuthData(out string login, out string password)
        {
            AuthDialog dialog = new AuthDialog();
            return dialog.ShowDialog(out login, out password);
        }

        private bool TryInputCode(out string code)
        {
            InputDialog dialog = new InputDialog();
            return dialog.ShowDialog("Enter code:", out code);
        }

        public bool Configure(IntPtr panelHandle)
        {
            OpenSettingsDialog();
            return true;
        }
        
        public void Uninstall()
        {
            string dataPath = MBApiInterface.Setting_GetPersistentStoragePath();
            string settingsDirPath = Path.Combine(dataPath, _settingsDirName);
            if (Directory.Exists(settingsDirPath))
                Directory.Delete(settingsDirPath, true);
        }

        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // ignore
        }


    }
}
