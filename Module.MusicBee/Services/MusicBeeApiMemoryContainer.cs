using System;
using System.Runtime.InteropServices;
using Module.MusicBee.Delegates;
using Module.MusicBee.Enums;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Module.MusicBee.Services
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MusicBeeApiMemoryContainer
    {
        public void Initialise(IntPtr apiInterfacePtr)
        {
            CopyMemory(ref this, apiInterfacePtr, 4);
            if (MusicBeeVersion == MusicBeeVersion.v2_0)
                // MusicBee version 2.0 - Api methods > revision 25 are not available
                CopyMemory(ref this, apiInterfacePtr, 456);
            else if (MusicBeeVersion == MusicBeeVersion.v2_1)
                CopyMemory(ref this, apiInterfacePtr, 516);
            else if (MusicBeeVersion == MusicBeeVersion.v2_2)
                CopyMemory(ref this, apiInterfacePtr, 584);
            else if (MusicBeeVersion == MusicBeeVersion.v2_3)
                CopyMemory(ref this, apiInterfacePtr, 596);
            else if (MusicBeeVersion == MusicBeeVersion.v2_4)
                CopyMemory(ref this, apiInterfacePtr, 604);
            else if (MusicBeeVersion == MusicBeeVersion.v2_5)
                CopyMemory(ref this, apiInterfacePtr, 648);
            else if (MusicBeeVersion == MusicBeeVersion.v3_0)
                CopyMemory(ref this, apiInterfacePtr, 652);
            else
                CopyMemory(ref this, apiInterfacePtr, Marshal.SizeOf(this));
        }
        
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("kernel32.dll")]
        private static extern void CopyMemory(ref MusicBeeApiMemoryContainer mbApi, IntPtr src, int length);
        
        public MusicBeeVersion MusicBeeVersion
        {
            get
            {
                return ApiRevision switch
                {
                    <= 25 => MusicBeeVersion.v2_0,
                    <= 31 => MusicBeeVersion.v2_1,
                    <= 33 => MusicBeeVersion.v2_2,
                    <= 38 => MusicBeeVersion.v2_3,
                    <= 43 => MusicBeeVersion.v2_4,
                    <= 47 => MusicBeeVersion.v2_5,
                    <= 48 => MusicBeeVersion.v3_0,
                    _ => MusicBeeVersion.v3_1
                };
            }
        }
        
        public short InterfaceVersion;
        public short ApiRevision;
        public MB_ReleaseStringDelegate MB_ReleaseString;
        public MB_TraceDelegate MB_Trace;
        public Setting_GetPersistentStoragePathDelegate Setting_GetPersistentStoragePath;
        public Setting_GetSkinDelegate Setting_GetSkin;
        public Setting_GetSkinElementColourDelegate Setting_GetSkinElementColour;
        public Setting_IsWindowBordersSkinnedDelegate Setting_IsWindowBordersSkinned;
        public Library_GetFilePropertyDelegate Library_GetFileProperty;
        public Library_GetFileTagDelegate Library_GetFileTag;
        public Library_SetFileTagDelegate Library_SetFileTag;
        public Library_CommitTagsToFileDelegate Library_CommitTagsToFile;
        public Library_GetLyricsDelegate Library_GetLyrics;
        [Obsolete("Use Library_GetArtworkEx")]
        public Library_GetArtworkDelegate Library_GetArtwork;
        public Library_QueryFilesDelegate Library_QueryFiles;
        public Library_QueryGetNextFileDelegate Library_QueryGetNextFile;
        public Player_GetPositionDelegate Player_GetPosition;
        public Player_SetPositionDelegate Player_SetPosition;
        public Player_GetPlayStateDelegate Player_GetPlayState;
        public Player_ActionDelegate Player_PlayPause;
        public Player_ActionDelegate Player_Stop;
        public Player_ActionDelegate Player_StopAfterCurrent;
        public Player_ActionDelegate Player_PlayPreviousTrack;
        public Player_ActionDelegate Player_PlayNextTrack;
        public Player_ActionDelegate Player_StartAutoDj;
        public Player_ActionDelegate Player_EndAutoDj;
        public Player_GetVolumeDelegate Player_GetVolume;
        public Player_SetVolumeDelegate Player_SetVolume;
        public Player_GetMuteDelegate Player_GetMute;
        public Player_SetMuteDelegate Player_SetMute;
        public Player_GetShuffleDelegate Player_GetShuffle;
        public Player_SetShuffleDelegate Player_SetShuffle;
        public Player_GetRepeatDelegate Player_GetRepeat;
        public Player_SetRepeatDelegate Player_SetRepeat;
        public Player_GetEqualiserEnabledDelegate Player_GetEqualiserEnabled;
        public Player_SetEqualiserEnabledDelegate Player_SetEqualiserEnabled;
        public Player_GetDspEnabledDelegate Player_GetDspEnabled;
        public Player_SetDspEnabledDelegate Player_SetDspEnabled;
        public Player_GetScrobbleEnabledDelegate Player_GetScrobbleEnabled;
        public Player_SetScrobbleEnabledDelegate Player_SetScrobbleEnabled;
        public NowPlaying_GetFileUrlDelegate NowPlaying_GetFileUrl;
        public NowPlaying_GetDurationDelegate NowPlaying_GetDuration;
        public NowPlaying_GetFilePropertyDelegate NowPlaying_GetFileProperty;
        public NowPlaying_GetFileTagDelegate NowPlaying_GetFileTag;
        public NowPlaying_GetLyricsDelegate NowPlaying_GetLyrics;
        public NowPlaying_GetArtworkDelegate NowPlaying_GetArtwork;
        public NowPlayingList_ActionDelegate NowPlayingList_Clear;
        public Library_QueryFilesDelegate NowPlayingList_QueryFiles;
        public Library_QueryGetNextFileDelegate NowPlayingList_QueryGetNextFile;
        public NowPlayingList_FileActionDelegate NowPlayingList_PlayNow;
        public NowPlayingList_FileActionDelegate NowPlayingList_QueueNext;
        public NowPlayingList_FileActionDelegate NowPlayingList_QueueLast;
        public NowPlayingList_ActionDelegate NowPlayingList_PlayLibraryShuffled;
        public Playlist_QueryPlaylistsDelegate Playlist_QueryPlaylists;
        public Playlist_QueryGetNextPlaylistDelegate Playlist_QueryGetNextPlaylist;
        public Playlist_GetTypeDelegate Playlist_GetType;
        public Playlist_QueryFilesDelegate Playlist_QueryFiles;
        public Library_QueryGetNextFileDelegate Playlist_QueryGetNextFile;
        public MB_WindowHandleDelegate MB_GetWindowHandle;
        public MB_RefreshPanelsDelegate MB_RefreshPanels;
        public MB_SendNotificationDelegate MB_SendNotification;
        public MB_AddMenuItemDelegate MB_AddMenuItem;
        public Setting_GetFieldNameDelegate Setting_GetFieldName;
        [Obsolete("Use Library_QueryFilesEx", true)]
        public Library_QueryGetAllFilesDelegate Library_QueryGetAllFiles;
        [Obsolete("Use NowPlayingList_QueryFilesEx", true)]
        public Library_QueryGetAllFilesDelegate NowPlayingList_QueryGetAllFiles;
        [Obsolete("Use Playlist_QueryFilesEx", true)]
        public Library_QueryGetAllFilesDelegate Playlist_QueryGetAllFiles;
        public MB_CreateBackgroundTaskDelegate MB_CreateBackgroundTask;
        public MB_SetBackgroundTaskMessageDelegate MB_SetBackgroundTaskMessage;
        public MB_RegisterCommandDelegate MB_RegisterCommand;
        public Setting_GetDefaultFontDelegate Setting_GetDefaultFont;
        public Player_GetShowTimeRemainingDelegate Player_GetShowTimeRemaining;
        public NowPlayingList_GetCurrentIndexDelegate NowPlayingList_GetCurrentIndex;
        public NowPlayingList_GetFileUrlDelegate NowPlayingList_GetListFileUrl;
        public NowPlayingList_GetFilePropertyDelegate NowPlayingList_GetFileProperty;
        public NowPlayingList_GetFileTagDelegate NowPlayingList_GetFileTag;
        public NowPlaying_GetSpectrumDataDelegate NowPlaying_GetSpectrumData;
        public NowPlaying_GetSoundGraphDelegate NowPlaying_GetSoundGraph;
        public MB_GetPanelBoundsDelegate MB_GetPanelBounds;
        public MB_AddPanelDelegate MB_AddPanel;
        public MB_RemovePanelDelegate MB_RemovePanel;
        public MB_GetLocalisationDelegate MB_GetLocalisation;
        public NowPlayingList_IsAnyPriorTracksDelegate NowPlayingList_IsAnyPriorTracks;
        public NowPlayingList_IsAnyFollowingTracksDelegate NowPlayingList_IsAnyFollowingTracks;
        public Player_ShowEqualiserDelegate Player_ShowEqualiser;
        public Player_GetAutoDjEnabledDelegate Player_GetAutoDjEnabled;
        public Player_GetStopAfterCurrentEnabledDelegate Player_GetStopAfterCurrentEnabled;
        public Player_GetCrossfadeDelegate Player_GetCrossfade;
        public Player_SetCrossfadeDelegate Player_SetCrossfade;
        public Player_GetReplayGainModeDelegate Player_GetReplayGainMode;
        public Player_SetReplayGainModeDelegate Player_SetReplayGainMode;
        public Player_QueueRandomTracksDelegate Player_QueueRandomTracks;
        public Setting_GetDataTypeDelegate Setting_GetDataType;
        public NowPlayingList_GetNextIndexDelegate NowPlayingList_GetNextIndex;
        public NowPlaying_GetArtistPictureDelegate NowPlaying_GetArtistPicture;
        public NowPlaying_GetArtworkDelegate NowPlaying_GetDownloadedArtwork;
        // api version 16
        public MB_ShowNowPlayingAssistantDelegate MB_ShowNowPlayingAssistant;
        // api version 17
        public NowPlaying_GetLyricsDelegate NowPlaying_GetDownloadedLyrics;
        // api version 18
        public Player_GetShowRatingTrackDelegate Player_GetShowRatingTrack;
        public Player_GetShowRatingLoveDelegate Player_GetShowRatingLove;
        // api version 19
        public MB_CreateParameterisedBackgroundTaskDelegate MB_CreateParameterisedBackgroundTask;
        public Setting_GetLastFmUserIdDelegate Setting_GetLastFmUserId;
        public Playlist_GetNameDelegate Playlist_GetName;
        public Playlist_CreatePlaylistDelegate Playlist_CreatePlaylist;
        public Playlist_SetFilesDelegate Playlist_SetFiles;
        public Library_QuerySimilarArtistsDelegate Library_QuerySimilarArtists;
        public Library_QueryLookupTableDelegate Library_QueryLookupTable;
        public Library_QueryGetLookupTableValueDelegate Library_QueryGetLookupTableValue;
        public NowPlayingList_FilesActionDelegate NowPlayingList_QueueFilesNext;
        public NowPlayingList_FilesActionDelegate NowPlayingList_QueueFilesLast;
        // api version 20
        public Setting_GetWebProxyDelegate Setting_GetWebProxy;
        // api version 21
        public NowPlayingList_RemoveAtDelegate NowPlayingList_RemoveAt;
        // api version 22
        public Playlist_RemoveAtDelegate Playlist_RemoveAt;
        // api version 23
        public MB_SetPanelScrollableAreaDelegate MB_SetPanelScrollableArea;
        // api version 24
        public MB_InvokeCommandDelegate MB_InvokeCommand;
        public MB_OpenFilterInTabDelegate MB_OpenFilterInTab;
        // api version 25
        public MB_SetWindowSizeDelegate MB_SetWindowSize;
        public Library_GetArtistPictureDelegate Library_GetArtistPicture;
        public Pending_GetFileUrlDelegate Pending_GetFileUrl;
        public Pending_GetFilePropertyDelegate Pending_GetFileProperty;
        public Pending_GetFileTagDelegate Pending_GetFileTag;
        // api version 26
        public Player_GetButtonEnabledDelegate Player_GetButtonEnabled;
        // api version 27
        public NowPlayingList_MoveFilesDelegate NowPlayingList_MoveFiles;
        // api version 28
        public Library_GetArtworkDelegate Library_GetArtworkUrl;
        public Library_GetArtistPictureThumbDelegate Library_GetArtistPictureThumb;
        public NowPlaying_GetArtworkDelegate NowPlaying_GetArtworkUrl;
        public NowPlaying_GetArtworkDelegate NowPlaying_GetDownloadedArtworkUrl;
        public NowPlaying_GetArtistPictureThumbDelegate NowPlaying_GetArtistPictureThumb;
        // api version 29
        public Playlist_IsInListDelegate Playlist_IsInList;
        // api version 30
        public Library_GetArtistPictureUrlsDelegate Library_GetArtistPictureUrls;
        public NowPlaying_GetArtistPictureUrlsDelegate NowPlaying_GetArtistPictureUrls;
        // api version 31
        public Playlist_AddFilesDelegate Playlist_AppendFiles;
        // api version 32
        public Sync_FileStartDelegate Sync_FileStart;
        public Sync_FileEndDelegate Sync_FileEnd;
        // api version 33
        public Library_QueryFilesExDelegate Library_QueryFilesEx;
        public Library_QueryFilesExDelegate NowPlayingList_QueryFilesEx;
        public Playlist_QueryFilesExDelegate Playlist_QueryFilesEx;
        public Playlist_MoveFilesDelegate Playlist_MoveFiles;
        public Playlist_PlayNowDelegate Playlist_PlayNow;
        public NowPlaying_IsSoundtrackDelegate NowPlaying_IsSoundtrack;
        public NowPlaying_GetArtistPictureUrlsDelegate NowPlaying_GetSoundtrackPictureUrls;
        public Library_GetDevicePersistentIdDelegate Library_GetDevicePersistentId;
        public Library_SetDevicePersistentIdDelegate Library_SetDevicePersistentId;
        public Library_FindDevicePersistentIdDelegate Library_FindDevicePersistentId;
        public Setting_GetValueDelegate Setting_GetValue;
        public Library_AddFileToLibraryDelegate Library_AddFileToLibrary;
        public Playlist_DeletePlaylistDelegate Playlist_DeletePlaylist;
        public Library_GetSyncDeltaDelegate Library_GetSyncDelta;
        // api version 35
        public Library_GetFileTagsDelegate Library_GetFileTags;
        public NowPlaying_GetFileTagsDelegate NowPlaying_GetFileTags;
        public NowPlayingList_GetFileTagsDelegate NowPlayingList_GetFileTags;
        // api version 43
        public MB_AddTreeNodeDelegate MB_AddTreeNode;
        public MB_DownloadFileDelegate MB_DownloadFile;
        // api version 47
        public Setting_GetFileConvertCommandLineDelegate Setting_GetFileConvertCommandLine;
        public Player_OpenStreamHandleDelegate Player_OpenStreamHandle;
        public Player_UpdatePlayStatisticsDelegate Player_UpdatePlayStatistics;
        public Library_GetArtworkExDelegate Library_GetArtworkEx;
        public Library_SetArtworkExDelegate Library_SetArtworkEx;
        public MB_GetVisualiserInformationDelegate MB_GetVisualiserInformation;
        public MB_ShowVisualiserDelegate MB_ShowVisualiser;
        public MB_GetPluginViewInformationDelegate MB_GetPluginViewInformation;
        public MB_ShowPluginViewDelegate MB_ShowPluginView;
        public Player_GetOutputDevicesDelegate Player_GetOutputDevices;
        public Player_SetOutputDeviceDelegate Player_SetOutputDevice;
        // api version 48
        public MB_UninistallPluginDelegate MB_UninstallPlugin;
        // api version 50
        public Player_ActionDelegate Player_PlayPreviousAlbum;
        public Player_ActionDelegate Player_PlayNextAlbum;
        // api version 51
        public Podcasts_QuerySubscriptionsDelegate Podcasts_QuerySubscriptions;
        public Podcasts_GetSubscriptionDelegate Podcasts_GetSubscription;
        public Podcasts_GetSubscriptionArtworkDelegate Podcasts_GetSubscriptionArtwork;
        public Podcasts_GetSubscriptionEpisodesDelegate Podcasts_GetSubscriptionEpisodes;
        public Podcasts_GetSubscriptionEpisodeDelegate Podcasts_GetSubscriptionEpisode;
        // api version 52
        public NowPlaying_GetSoundGraphExDelegate NowPlaying_GetSoundGraphEx;
        // api version 53
        public Sync_FileStartDelegate Sync_FileDeleteStart;
        public Sync_FileEndDelegate Sync_FileDeleteEnd;
    }
}