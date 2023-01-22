using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Module.MusicBee.MetaInfo.Entities;
using Module.MusicBee.MetaInfo.Enums;
using Module.MusicBee.MetaInfo.Extensions;
using Module.MusicBee.Services;
using Root.Helpers;

namespace Module.MusicBee.MetaInfo.Helpers;

public static class MetaInfoProvider
{
    private static readonly IReadOnlyCollection<MethodNameWithRestriction> MethodNames =
        new MethodNameWithRestriction[]
        {
            new("MB_ReleaseString"),
            new("MB_Trace"),
            new("Setting_GetPersistentStoragePath"),
            new("Setting_GetSkin"),
            new("Setting_GetSkinElementColour"),
            new("Setting_IsWindowBordersSkinned"),
            new("Library_GetFileProperty"),
            new("Library_GetFileTag"),
            new("Library_SetFileTag"),
            new("Library_CommitTagsToFile"),
            new("Library_GetLyrics"),
            // obsolete
            new("Library_GetArtwork", MethodRestriction.Ignore),
            new("Library_QueryFiles"),
            new("Library_QueryGetNextFile"),
            new("Player_GetPosition"),
            new("Player_SetPosition"),
            new("Player_GetPlayState"),
            new("Player_PlayPause"),
            new("Player_Stop"),
            new("Player_StopAfterCurrent"),
            new("Player_PlayPreviousTrack"),
            new("Player_PlayNextTrack"),
            new("Player_StartAutoDj"),
            new("Player_EndAutoDj"),
            new("Player_GetVolume"),
            new("Player_SetVolume"),
            new("Player_GetMute"),
            new("Player_SetMute"),
            new("Player_GetShuffle"),
            new("Player_SetShuffle"),
            new("Player_GetRepeat"),
            new("Player_SetRepeat"),
            new("Player_GetEqualiserEnabled"),
            new("Player_SetEqualiserEnabled"),
            new("Player_GetDspEnabled"),
            new("Player_SetDspEnabled"),
            new("Player_GetScrobbleEnabled"),
            new("Player_SetScrobbleEnabled"),
            new("NowPlaying_GetFileUrl"),
            new("NowPlaying_GetDuration"),
            new("NowPlaying_GetFileProperty"),
            new("NowPlaying_GetFileTag"),
            new("NowPlaying_GetLyrics"),
            new("NowPlaying_GetArtwork"),
            new("NowPlayingList_Clear"),
            new("NowPlayingList_QueryFiles"),
            new("NowPlayingList_QueryGetNextFile"),
            new("NowPlayingList_PlayNow"),
            new("NowPlayingList_QueueNext"),
            new("NowPlayingList_QueueLast"),
            new("NowPlayingList_PlayLibraryShuffled"),
            new("Playlist_QueryPlaylists"),
            new("Playlist_QueryGetNextPlaylist"),
            new("Playlist_GetType"),
            new("Playlist_QueryFiles"),
            new("Playlist_QueryGetNextFile"),
            // IntPtr
            new("MB_GetWindowHandle", MethodRestriction.Extended),
            new("MB_RefreshPanels"),
            new("MB_SendNotification"),
            // EventHandler
            new("MB_AddMenuItem", MethodRestriction.Extended),
            new("Setting_GetFieldName"),
            // obsolete
            new("Library_QueryGetAllFiles", MethodRestriction.Ignore),
            // obsolete
            new("NowPlayingList_QueryGetAllFiles", MethodRestriction.Ignore),
            // obsolete
            new("Playlist_QueryGetAllFiles", MethodRestriction.Ignore),
            // ThreadStart
            new("MB_CreateBackgroundTask", MethodRestriction.Extended),
            new("MB_SetBackgroundTaskMessage"),
            // EventHandler
            new("MB_RegisterCommand", MethodRestriction.Extended),
            // Font
            new("Setting_GetDefaultFont", MethodRestriction.Extended),
            new("Player_GetShowTimeRemaining"),
            new("NowPlayingList_GetCurrentIndex"),
            new("NowPlayingList_GetListFileUrl"),
            new("NowPlayingList_GetFileProperty"),
            new("NowPlayingList_GetFileTag"),
            new("NowPlaying_GetSpectrumData"),
            new("NowPlaying_GetSoundGraph"),
            // Rectangle
            new("MB_GetPanelBounds", MethodRestriction.Extended),
            // Control
            new("MB_AddPanel", MethodRestriction.Extended),
            // Control
            new("MB_RemovePanel", MethodRestriction.Extended),
            new("MB_GetLocalisation"),
            new("NowPlayingList_IsAnyPriorTracks"),
            new("NowPlayingList_IsAnyFollowingTrac"),
            new("Player_ShowEqualiser"),
            new("Player_GetAutoDjEnabled"),
            new("Player_GetStopAfterCurrentEnabled"),
            new("Player_GetCrossfade"),
            new("Player_SetCrossfade"),
            new("Player_GetReplayGainMode"),
            new("Player_SetReplayGainMode"),
            new("Player_QueueRandomTracks"),
            new("Setting_GetDataType"),
            new("NowPlayingList_GetNextIndex"),
            new("NowPlaying_GetArtistPicture"),
            new("NowPlaying_GetDownloadedArtwork"),
            new("MB_ShowNowPlayingAssistant"),
            new("NowPlaying_GetDownloadedLyrics"),
            new("Player_GetShowRatingTrack"),
            new("Player_GetShowRatingLove"),
            // ParameterizedThreadStart
            new("MB_CreateParameterisedBackgroundTask", MethodRestriction.Extended),
            new("Setting_GetLastFmUserId"),
            new("Playlist_GetName"),
            new("Playlist_CreatePlaylist"),
            new("Playlist_SetFiles"),
            new("Library_QuerySimilarArtists"),
            new("Library_QueryLookupTable"),
            new("Library_QueryGetLookupTableValue"),
            new("NowPlayingList_QueueFilesNext"),
            new("NowPlayingList_QueueFilesLast"),
            new("Setting_GetWebProxy"),
            new("NowPlayingList_RemoveAt"),
            new("Playlist_RemoveAt"),
            // Control
            new("MB_SetPanelScrollableArea", MethodRestriction.Extended),
            // object
            new("MB_InvokeCommand", MethodRestriction.Extended),
            new("MB_OpenFilterInTab"),
            new("MB_SetWindowSize"),
            new("Library_GetArtistPicture"),
            new("Pending_GetFileUrl"),
            new("Pending_GetFileProperty"),
            new("Pending_GetFileTag"),
            new("Player_GetButtonEnabled"),
            new("NowPlayingList_MoveFiles"),
            new("Library_GetArtworkUrl"),
            new("Library_GetArtistPictureThumb"),
            new("NowPlaying_GetArtworkUrl"),
            new("NowPlaying_GetDownloadedArtworkUrl"),
            new("NowPlaying_GetArtistPictureThumb"),
            new("Playlist_IsInList"),
            new("Library_GetArtistPictureUrls"),
            new("NowPlaying_GetArtistPictureUrls"),
            new("Playlist_AppendFiles"),
            new("Sync_FileStart"),
            new("Sync_FileEnd"),
            new("Library_QueryFilesEx"),
            new("NowPlayingList_QueryFilesEx"),
            new("Playlist_QueryFilesEx"),
            new("Playlist_MoveFiles"),
            new("Playlist_PlayNow"),
            new("NowPlaying_IsSoundtrack"),
            new("NowPlaying_GetSoundtrackPictureUrls"),
            new("Library_GetDevicePersistentId"),
            new("Library_SetDevicePersistentId"),
            new("Library_FindDevicePersistentId"),
            // object
            new("Setting_GetValue", MethodRestriction.Extended),
            new("Library_AddFileToLibrary"),
            new("Playlist_DeletePlaylist"),
            // DateTime - ради одного метода не хочеться что-то пилить
            new("Library_GetSyncDelta", MethodRestriction.Extended),
            new("Library_GetFileTags"),
            new("NowPlaying_GetFileTags"),
            new("NowPlayingList_GetFileTags"),
            // EventHandler
            new("MB_AddTreeNode", MethodRestriction.Extended),
            new("MB_DownloadFile"),
            new("Setting_GetFileConvertCommandLine"),
            new("Player_OpenStreamHandle"),
            new("Player_UpdatePlayStatistics"),
            new("Library_GetArtworkEx"),
            new("Library_SetArtworkEx"),
            new("MB_GetVisualiserInformation"),
            new("MB_ShowVisualiser"),
            new("MB_GetPluginViewInformation"),
            new("MB_ShowPluginView"),
            new("Player_GetOutputDevices"),
            new("Player_SetOutputDevice"),
            new("MB_UninstallPlugin"),
            new("Player_PlayPreviousAlbum"),
            new("Player_PlayNextAlbum"),
            new("Podcasts_QuerySubscriptions"),
            new("Podcasts_GetSubscription"),
            new("Podcasts_GetSubscriptionArtwork"),
            new("Podcasts_GetSubscriptionEpisodes"),
            new("Podcasts_GetSubscriptionEpisode"),
            new("NowPlaying_GetSoundGraphEx"),
            new("Sync_FileDeleteStart"),
            new("Sync_FileDeleteEnd"),
        };

    public static readonly IReadOnlyCollection<string> MethodNamesWithoutRestrictions = MethodNames
        .Where(x => x.Restriction == MethodRestriction.None)
        .Select(x => x.MethodName)
        .ToReadOnlyCollection();

    public static readonly IReadOnlyCollection<string> ExtendedMethodNames = MethodNames
        .Where(x => x.Restriction == MethodRestriction.Extended)
        .Select(x => x.MethodName)
        .ToReadOnlyCollection();

    public static readonly IReadOnlyCollection<string> MethodNamesExceptIgnored = MethodNames
        .Where(x => x.Restriction != MethodRestriction.Ignore)
        .Select(x => x.MethodName)
        .ToReadOnlyCollection();

    public static IReadOnlyCollection<MethodDefinition> GetMethodsDefinitions(
        IReadOnlyCollection<string> methods)
    {
        return typeof(MusicBeeApiMemoryContainer)
            .GetMembers()
            .Where(x => methods.Contains(x.Name))
            .CastOrSkip<MemberInfo, FieldInfo>()
            .Select(GetMethodDefinition)
            .ToReadOnlyCollection();
    }

    private static MethodDefinition GetMethodDefinition(FieldInfo delegateFieldInfo)
    {
        var name = delegateFieldInfo.Name;

        var (returnParameter, parameters) = GetDelegateFieldParameters(delegateFieldInfo);

        var inputParameters = parameters
            .Where(x => !x.IsOut)
            .Select(GetParameterDefinition)
            .ToReadOnlyCollection();

        var outputParameters = parameters
            .Where(x => x.IsOut)
            .Select(GetParameterDefinition)
            .ToReadOnlyCollection();

        return new MethodDefinition(
            name,
            inputParameters,
            outputParameters,
            new ParameterDefinition(
                returnParameter.ParameterType,
                string.Empty,
                returnParameter.IsNullable()
            )
        );
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


    private static ParameterDefinition GetParameterDefinition(ParameterInfo parameterInfo)
    {
        return new ParameterDefinition(
            parameterInfo.ParameterType.RemoveRefWrapper(),
            parameterInfo.Name,
            parameterInfo.IsNullable()
        );
    }
}