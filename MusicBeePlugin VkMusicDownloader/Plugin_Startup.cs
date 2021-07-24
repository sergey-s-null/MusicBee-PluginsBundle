using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using VkMusicDownloader;
using VkMusicDownloader.Ex;
using VkMusicDownloader.GUI;
using VkNet;
using VkNet.AudioBypassService.Extensions;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        private IReadOnlyKernel _kernel;
        private MusicBeeApiInterface _mbApi;
        private VkApi _vkApi;
        
        public static Settings Settings;
        private static string _settingsDirName = "Laiser399_VkAudioDownloader";
        private static string _settingsFileName = "settings.json";

        // private PluginInfo about = new PluginInfo();

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            ServicePointManager.DefaultConnectionLimit = 20;

            _mbApi = new MusicBeeApiInterface();
            _mbApi.Initialise(apiInterfacePtr);
            
            _kernel = Bootstrapper.GetKernel(_mbApi);
            _vkApi = _kernel.Get<VkApi>();
            
            CreateSettingsDirectory();
            InitSettings();

            _mbApi.MB_AddMenuItem("mnuTools/Laiser399: download vk audio",
                "Laiser399: download vk audio", (_, _) => OpenDownloadDialog());

            return GetPluginInfo();
        }

        private static PluginInfo GetPluginInfo()
        {
            return new()
            {
                PluginInfoVersion = PluginInfoVersion,
                Name = "Laiser399: VK audios downloader",
                Description = "Download audios from vk and set custom1, custom2 tags",
                Author = "Laiser399",
                TargetApplication = "", //  the name of a Plugin Storage device or panel header for a dockable panel
                Type = PluginType.General,
                VersionMajor = 1, // your plugin version
                VersionMinor = 0,
                Revision = 1,
                MinInterfaceVersion = MinInterfaceVersion,
                MinApiRevision = MinApiRevision,
                ReceiveNotifications = ReceiveNotificationFlags.StartupOnly,
                ConfigurationPanelHeight = 0
            };
        }

        private void CreateSettingsDirectory()
        {
            var dataPath = _mbApi.Setting_GetPersistentStoragePath();
            var settingsDirPath = Path.Combine(dataPath, _settingsDirName);
            if (!Directory.Exists(settingsDirPath))
                Directory.CreateDirectory(settingsDirPath);
        }

        private void InitSettings()
        {
            var dataPath = _mbApi.Setting_GetPersistentStoragePath();
            var settingsFilePath = Path.Combine(dataPath, _settingsDirName, _settingsFileName);
            Settings = new Settings(settingsFilePath);
        }
        
        private async void OpenDownloadDialog()
        {
            if (!_vkApi.IsAuthorized)
            {
                string token = Settings.AccessToken;

                if (!_vkApi.TryAuth(token))
                {
                    if (await _vkApi.TryAuthAsync(TryInputAuthData, TryInputCode))
                    {
                        Settings.AccessToken = _vkApi.Token;
                        Settings.Save();
                    }
                    else
                    {
                        MessageBox.Show("Auth error.");
                        return;
                    }
                }
            }

            var downloadDialog = _kernel.Get<MainWindow>();
            downloadDialog.ShowDialog();
        }

        private void OpenSettingsDialog()
        {
            var settingsDialog = new SettingsDialog();
            settingsDialog.ShowDialog();
        }

        private bool TryInputAuthData(out string login, out string password)
        {
            var dialog = new AuthDialog();
            return dialog.ShowDialog(out login, out password);
        }

        private bool TryInputCode(out string code)
        {
            var dialog = new InputDialog();
            return dialog.ShowDialog("Enter code:", out code);
        }

        public bool Configure(IntPtr panelHandle)
        {
            OpenSettingsDialog();
            return true;
        }
        
        public void Uninstall()
        {
            var dataPath = _mbApi.Setting_GetPersistentStoragePath();
            var settingsDirPath = Path.Combine(dataPath, _settingsDirName);
            try
            {
                if (Directory.Exists(settingsDirPath))
                    Directory.Delete(settingsDirPath, true);
            }
            catch { }
        }

        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // ignore
        }

        // static
        private const int _audiosPerBlock = 20;
        public static void CalcIndices(int index, out int index1, out int index2)
        {
            index1 = index / _audiosPerBlock + 1;
            index2 = index % _audiosPerBlock + 1;
        }
    }
}
