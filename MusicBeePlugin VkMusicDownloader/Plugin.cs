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
using VkMusicDownloader;
using VkMusicDownloader.Ex;
using VkMusicDownloader.GUI;
using VkNet;
using VkNet.AudioBypassService.Extensions;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        public static MusicBeeApiInterface MBApiInterface;
        public static Settings Settings;
        private static string _settingsDirName = "Laiser399_VkAudioDownloader";
        private static string _settingsFileName = "settings.json";

        private PluginInfo about = new PluginInfo();
        private VkApi _vkApi;

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

        private void InitVkApi()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();
            _vkApi = new VkApi(serviceCollection);
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

            var downloadDialog = new MainWindow() { VkApi = _vkApi };
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

        //   custom tags protocol
        private static MetaDataType _vkIdField = MetaDataType.Custom3;
        private static MetaDataType _indexField = MetaDataType.Custom4;
        private static MetaDataType _index1Field = MetaDataType.Custom1;
        private static MetaDataType _index2Field = MetaDataType.Custom2;

        public static bool TryGetVkId(string filePath, out long id)
        {
            string idStr = MBApiInterface.Library_GetFileTag(filePath, _vkIdField);
            return long.TryParse(idStr, out id);
        }

        public static bool SetVkId(string filePath, long id, bool commit = true)
        {
            bool res = MBApiInterface.Library_SetFileTag(filePath, _vkIdField, id.ToString());
            if (!res)
                return false;

            if (commit)
                return MBApiInterface.Library_CommitTagsToFile(filePath);
            else
                return true;
        }

        public static bool TryGetIndex(string filePath, out int index)
        {
            string indexStr = MBApiInterface.Library_GetFileTag(filePath, _indexField);
            if (int.TryParse(indexStr, out index))
                return true;
            else
            {
                index = -1;
                return false;
            }
        }

        public static bool SetIndex(string filePath, int index, bool commit = true)
        {
            bool res = MBApiInterface.Library_SetFileTag(filePath, _indexField, index.ToString());
            if (!res)
                return false;

            if (commit)
                return MBApiInterface.Library_CommitTagsToFile(filePath);
            else
                return true;
        }

        public static bool TryGetIndex1(string filePath, out int index1)
        {
            string index1Str = MBApiInterface.Library_GetFileTag(filePath, _index1Field);
            return int.TryParse(index1Str, out index1);
        }

        public static bool SetIndex1(string filePath, int index1, bool commit = true)
        {
            string i1Str = index1.ToString().PadLeft(2, '0');
            bool res = MBApiInterface.Library_SetFileTag(filePath, _index1Field, i1Str);
            if (!res)
                return false;

            if (commit)
                return MBApiInterface.Library_CommitTagsToFile(filePath);
            else
                return true;
        }

        public static bool TryGetIndex2(string filePath, out int index2)
        {
            string index2Str = MBApiInterface.Library_GetFileTag(filePath, _index2Field);
            return int.TryParse(index2Str, out index2);
        }

        public static bool SetIndex2(string filePath, int index2, bool commit = true)
        {
            string i2Str = index2.ToString().PadLeft(2, '0');
            bool res = MBApiInterface.Library_SetFileTag(filePath, _index2Field, i2Str);
            if (!res)
                return false;

            if (commit)
                return MBApiInterface.Library_CommitTagsToFile(filePath);
            else
                return true;
        }
    }
}
