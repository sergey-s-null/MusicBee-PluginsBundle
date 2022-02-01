using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MBApiProtoGenerator.Builders;
using MBApiProtoGenerator.Enums;
using MBApiProtoGenerator.Models;
using Microsoft.Build.Evaluation;
using Root;
using Root.Helpers;

namespace MBApiProtoGenerator
{
    internal class Program
    {
        private const string ProjectPath = @"..\..\..\Module.RemoteMusicBeeApi";
        private const string ExportPathInsideProject = "Protos";

        private const string ServiceName = "MusicBeeApiService";

        private const string CsProjFilePath = ProjectPath + @"\Module.RemoteMusicBeeApi.csproj";

        private static readonly IReadOnlyCollection<string> FieldsToExport = new[]
        {
            "MB_ReleaseString",
            "MB_Trace",
            "Setting_GetPersistentStoragePath",
            "Setting_GetSkin",
            "Setting_GetSkinElementColour",
            "Setting_IsWindowBordersSkinned",
            "Library_GetFileProperty",
            "Library_GetFileTag",
            "Library_SetFileTag",
            "Library_CommitTagsToFile",
            "Library_GetLyrics",
            "Library_GetArtwork - obsolete",
            "Library_QueryFiles",
            "Library_QueryGetNextFile",
            "Player_GetPosition",
            "Player_SetPosition",
            "Player_GetPlayState",
            "Player_PlayPause",
            "Player_Stop",
            "Player_StopAfterCurrent",
            "Player_PlayPreviousTrack",
            "Player_PlayNextTrack",
            "Player_StartAutoDj",
            "Player_EndAutoDj",
            "Player_GetVolume",
            "Player_SetVolume",
            "Player_GetMute",
            "Player_SetMute",
            "Player_GetShuffle",
            "Player_SetShuffle",
            "Player_GetRepeat",
            "Player_SetRepeat",
            "Player_GetEqualiserEnabled",
            "Player_SetEqualiserEnabled",
            "Player_GetDspEnabled",
            "Player_SetDspEnabled",
            "Player_GetScrobbleEnabled",
            "Player_SetScrobbleEnabled",
            "NowPlaying_GetFileUrl",
            "NowPlaying_GetDuration",
            "NowPlaying_GetFileProperty",
            "NowPlaying_GetFileTag",
            "NowPlaying_GetLyrics",
            "NowPlaying_GetArtwork",
            "NowPlayingList_Clear",
            "NowPlayingList_QueryFiles",
            "NowPlayingList_QueryGetNextFile",
            "NowPlayingList_PlayNow",
            "NowPlayingList_QueueNext",
            "NowPlayingList_QueueLast",
            "NowPlayingList_PlayLibraryShuffled",
            "Playlist_QueryPlaylists",
            "Playlist_QueryGetNextPlaylist",
            "Playlist_GetType",
            "Playlist_QueryFiles",
            "Playlist_QueryGetNextFile",
            // IntPtr
            // "MB_GetWindowHandle",
            "MB_RefreshPanels",
            "MB_SendNotification",
            // EventHandler
            // "MB_AddMenuItem",
            "Setting_GetFieldName",
            "Library_QueryGetAllFiles",
            "NowPlayingList_QueryGetAllFiles",
            "Playlist_QueryGetAllFiles",
            // ThreadStart
            // "MB_CreateBackgroundTask",
            "MB_SetBackgroundTaskMessage",
            // EventHandler
            // "MB_RegisterCommand",
            // Font
            // "Setting_GetDefaultFont",
            "Player_GetShowTimeRemaining",
            "NowPlayingList_GetCurrentIndex",
            "NowPlayingList_GetListFileUrl",
            "NowPlayingList_GetFileProperty",
            "NowPlayingList_GetFileTag",
            "NowPlaying_GetSpectrumData",
            "NowPlaying_GetSoundGraph",
            // Rectangle
            // "MB_GetPanelBounds",
            // Control
            // "MB_AddPanel",
            // Control
            // "MB_RemovePanel",
            "MB_GetLocalisation",
            "NowPlayingList_IsAnyPriorTracks",
            "NowPlayingList_IsAnyFollowingTrac",
            "Player_ShowEqualiser",
            "Player_GetAutoDjEnabled",
            "Player_GetStopAfterCurrentEnabled",
            "Player_GetCrossfade",
            "Player_SetCrossfade",
            "Player_GetReplayGainMode",
            "Player_SetReplayGainMode",
            "Player_QueueRandomTracks",
            "Setting_GetDataType",
            "NowPlayingList_GetNextIndex",
            "NowPlaying_GetArtistPicture",
            "NowPlaying_GetDownloadedArtwork",
            "MB_ShowNowPlayingAssistant",
            "NowPlaying_GetDownloadedLyrics",
            "Player_GetShowRatingTrack",
            "Player_GetShowRatingLove",
            // ParameterizedThreadStart
            // "MB_CreateParameterisedBackgroundTask",
            "Setting_GetLastFmUserId",
            "Playlist_GetName",
            "Playlist_CreatePlaylist",
            "Playlist_SetFiles",
            "Library_QuerySimilarArtists",
            "Library_QueryLookupTable",
            "Library_QueryGetLookupTableValue",
            "NowPlayingList_QueueFilesNext",
            "NowPlayingList_QueueFilesLast",
            "Setting_GetWebProxy",
            "NowPlayingList_RemoveAt",
            "Playlist_RemoveAt",
            // Control
            // "MB_SetPanelScrollableArea",
            // object
            // "MB_InvokeCommand",
            "MB_OpenFilterInTab",
            "MB_SetWindowSize",
            "Library_GetArtistPicture",
            "Pending_GetFileUrl",
            "Pending_GetFileProperty",
            "Pending_GetFileTag",
            "Player_GetButtonEnabled",
            "NowPlayingList_MoveFiles",
            "Library_GetArtworkUrl",
            "Library_GetArtistPictureThumb",
            "NowPlaying_GetArtworkUrl",
            "NowPlaying_GetDownloadedArtworkUrl",
            "NowPlaying_GetArtistPictureThumb",
            "Playlist_IsInList",
            "Library_GetArtistPictureUrls",
            "NowPlaying_GetArtistPictureUrls",
            "Playlist_AppendFiles",
            "Sync_FileStart",
            "Sync_FileEnd",
            "Library_QueryFilesEx",
            "NowPlayingList_QueryFilesEx",
            "Playlist_QueryFilesEx",
            "Playlist_MoveFiles",
            "Playlist_PlayNow",
            "NowPlaying_IsSoundtrack",
            "NowPlaying_GetSoundtrackPictureUrls",
            "Library_GetDevicePersistentId",
            "Library_SetDevicePersistentId",
            "Library_FindDevicePersistentId",
            // object
            // "Setting_GetValue",
            "Library_AddFileToLibrary",
            "Playlist_DeletePlaylist",
            // DateTime - ради одного метода не хочеться что-то пилить
            // "Library_GetSyncDelta",
            "Library_GetFileTags",
            "NowPlaying_GetFileTags",
            "NowPlayingList_GetFileTags",
            // EventHandler
            // "MB_AddTreeNode",
            "MB_DownloadFile",
            "Setting_GetFileConvertCommandLine",
            "Player_OpenStreamHandle",
            "Player_UpdatePlayStatistics",
            "Library_GetArtworkEx",
            "Library_SetArtworkEx",
            "MB_GetVisualiserInformation",
            "MB_ShowVisualiser",
            "MB_GetPluginViewInformation",
            "MB_ShowPluginView",
            "Player_GetOutputDevices",
            "Player_SetOutputDevice",
            "MB_UninstallPlugin",
            "Player_PlayPreviousAlbum",
            "Player_PlayNextAlbum",
            "Podcasts_QuerySubscriptions",
            "Podcasts_GetSubscription",
            "Podcasts_GetSubscriptionArtwork",
            "Podcasts_GetSubscriptionEpisodes",
            "Podcasts_GetSubscriptionEpisode",
            "NowPlaying_GetSoundGraphEx",
            "Sync_FileDeleteStart",
            "Sync_FileDeleteEnd",
        };

        public static void Main(string[] args)
        {
            var apiType = typeof(MusicBeeApiInterface);
            var methods = apiType
                .GetMembers()
                .Where(x => FieldsToExport.Contains(x.Name))
                .CastOrSkip<MemberInfo, FieldInfo>()
                .Select(Define)
                .ToReadOnlyCollection();

            new ProtoFilesBuilder()
                .SetPostfixes("_Request", "_Response")
                .SetExportPath(ProjectPath, ExportPathInsideProject)
                .SetReturnParameterName("result")
                .AddMethods(methods)
                .DeleteCurrentProtoFiles()
                .CreateMessagesProtoFiles()
                .CreateServiceProtoFile(ServiceName);

            AddProtobufToCsProj(methods);
        }

        private static void AddProtobufToCsProj(IEnumerable<MBApiMethodDefinition> methods)
        {
            var projectCollection = new ProjectCollection();
            var project = projectCollection.LoadProject(CsProjFilePath);

            var filePaths = methods
                .Select(x => @$"{ExportPathInsideProject}\{x.Name}.proto")
                .Append(@$"{ExportPathInsideProject}\{ServiceName}.proto");

            new CsProjProtobufBuilder(project)
                .RemoveAllProtobuf()
                .SetProtobufType(ProtobufType.Server)
                .AddProtobufItemGroup(filePaths)
                .SaveProject();
        }

        private static MBApiMethodDefinition Define(FieldInfo delegateFieldInfo)
        {
            var name = delegateFieldInfo.Name;

            var (returnParameter, parameters) = GetDelegateFieldParameters(delegateFieldInfo);

            var inputParameters = parameters
                .Where(x => !x.IsOut)
                .Select(Define)
                .ToReadOnlyCollection();

            var outputParameters = parameters
                .Where(x => x.IsOut)
                .Select(Define)
                .ToReadOnlyCollection();

            return new MBApiMethodDefinition(name, inputParameters, outputParameters, returnParameter.ParameterType);
        }

        private static (ParameterInfo returnParameter, ParameterInfo[] parameters) GetDelegateFieldParameters(
            FieldInfo delegateFieldInfo)
        {
            var invokeMethod = delegateFieldInfo.FieldType.GetMethod("Invoke");
            if (invokeMethod is null)
            {
                throw new Exception($"Invoke method of delegate field {delegateFieldInfo.Name} is null.");
            }

            return (invokeMethod.ReturnParameter, invokeMethod.GetParameters());
        }

        private static MBApiParameterDefinition Define(ParameterInfo parameterInfo)
        {
            return new MBApiParameterDefinition(parameterInfo.ParameterType, parameterInfo.Name);
        }
    }
}