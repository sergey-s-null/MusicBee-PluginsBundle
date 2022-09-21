using System.Linq;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;

namespace ConsoleTests.Services
{
    public class MusicBeeApiClientWrapper : IBaseMusicBeeApi
    {
        private readonly MusicBeeApiService.MusicBeeApiServiceClient _client;
        
        public MusicBeeApiClientWrapper(MusicBeeApiService.MusicBeeApiServiceClient client)
        {
            _client = client;
        }
        
        public void MB_ReleaseString(string p1)
        {
            _client.MB_ReleaseString(new MB_ReleaseString_Request
            {
                P1 = p1,
            });
        }
        
        public void MB_Trace(string p1)
        {
            _client.MB_Trace(new MB_Trace_Request
            {
                P1 = p1,
            });
        }
        
        public string Setting_GetPersistentStoragePath()
        {
            var response = _client.Setting_GetPersistentStoragePath(new Empty());
            return response.Result;
        }
        
        public string Setting_GetSkin()
        {
            var response = _client.Setting_GetSkin(new Empty());
            return response.Result;
        }
        
        public int Setting_GetSkinElementColour(SkinElement element, ElementState state, ElementComponent component)
        {
            var response = _client.Setting_GetSkinElementColour(new Setting_GetSkinElementColour_Request
            {
                Element = (int)element,
                State = (int)state,
                Component = (int)component,
            });
            return response.Result;
        }
        
        public bool Setting_IsWindowBordersSkinned()
        {
            var response = _client.Setting_IsWindowBordersSkinned(new Empty());
            return response.Result;
        }
        
        public string Library_GetFileProperty(string sourceFileUrl, FilePropertyType type)
        {
            var response = _client.Library_GetFileProperty(new Library_GetFileProperty_Request
            {
                SourceFileUrl = sourceFileUrl,
                Type = (int)type,
            });
            return response.Result;
        }
        
        public string Library_GetFileTag(string sourceFileUrl, MetaDataType field)
        {
            var response = _client.Library_GetFileTag(new Library_GetFileTag_Request
            {
                SourceFileUrl = sourceFileUrl,
                Field = (int)field,
            });
            return response.Result;
        }
        
        public bool Library_SetFileTag(string sourceFileUrl, MetaDataType field, string value)
        {
            var response = _client.Library_SetFileTag(new Library_SetFileTag_Request
            {
                SourceFileUrl = sourceFileUrl,
                Field = (int)field,
                Value = value,
            });
            return response.Result;
        }
        
        public bool Library_CommitTagsToFile(string sourceFileUrl)
        {
            var response = _client.Library_CommitTagsToFile(new Library_CommitTagsToFile_Request
            {
                SourceFileUrl = sourceFileUrl,
            });
            return response.Result;
        }
        
        public string Library_GetLyrics(string sourceFileUrl, LyricsType type)
        {
            var response = _client.Library_GetLyrics(new Library_GetLyrics_Request
            {
                SourceFileUrl = sourceFileUrl,
                Type = (int)type,
            });
            return response.Result;
        }
        
        public bool Library_QueryFiles(string query)
        {
            var response = _client.Library_QueryFiles(new Library_QueryFiles_Request
            {
                Query = query,
            });
            return response.Result;
        }
        
        public string Library_QueryGetNextFile()
        {
            var response = _client.Library_QueryGetNextFile(new Empty());
            return response.Result;
        }
        
        public int Player_GetPosition()
        {
            var response = _client.Player_GetPosition(new Empty());
            return response.Result;
        }
        
        public bool Player_SetPosition(int position)
        {
            var response = _client.Player_SetPosition(new Player_SetPosition_Request
            {
                Position = position,
            });
            return response.Result;
        }
        
        public PlayState Player_GetPlayState()
        {
            var response = _client.Player_GetPlayState(new Empty());
            return (PlayState)response.Result;
        }
        
        public bool Player_PlayPause()
        {
            var response = _client.Player_PlayPause(new Empty());
            return response.Result;
        }
        
        public bool Player_Stop()
        {
            var response = _client.Player_Stop(new Empty());
            return response.Result;
        }
        
        public bool Player_StopAfterCurrent()
        {
            var response = _client.Player_StopAfterCurrent(new Empty());
            return response.Result;
        }
        
        public bool Player_PlayPreviousTrack()
        {
            var response = _client.Player_PlayPreviousTrack(new Empty());
            return response.Result;
        }
        
        public bool Player_PlayNextTrack()
        {
            var response = _client.Player_PlayNextTrack(new Empty());
            return response.Result;
        }
        
        public bool Player_StartAutoDj()
        {
            var response = _client.Player_StartAutoDj(new Empty());
            return response.Result;
        }
        
        public bool Player_EndAutoDj()
        {
            var response = _client.Player_EndAutoDj(new Empty());
            return response.Result;
        }
        
        public float Player_GetVolume()
        {
            var response = _client.Player_GetVolume(new Empty());
            return response.Result;
        }
        
        public bool Player_SetVolume(float volume)
        {
            var response = _client.Player_SetVolume(new Player_SetVolume_Request
            {
                Volume = volume,
            });
            return response.Result;
        }
        
        public bool Player_GetMute()
        {
            var response = _client.Player_GetMute(new Empty());
            return response.Result;
        }
        
        public bool Player_SetMute(bool mute)
        {
            var response = _client.Player_SetMute(new Player_SetMute_Request
            {
                Mute = mute,
            });
            return response.Result;
        }
        
        public bool Player_GetShuffle()
        {
            var response = _client.Player_GetShuffle(new Empty());
            return response.Result;
        }
        
        public bool Player_SetShuffle(bool shuffle)
        {
            var response = _client.Player_SetShuffle(new Player_SetShuffle_Request
            {
                Shuffle = shuffle,
            });
            return response.Result;
        }
        
        public RepeatMode Player_GetRepeat()
        {
            var response = _client.Player_GetRepeat(new Empty());
            return (RepeatMode)response.Result;
        }
        
        public bool Player_SetRepeat(RepeatMode repeat)
        {
            var response = _client.Player_SetRepeat(new Player_SetRepeat_Request
            {
                Repeat = (int)repeat,
            });
            return response.Result;
        }
        
        public bool Player_GetEqualiserEnabled()
        {
            var response = _client.Player_GetEqualiserEnabled(new Empty());
            return response.Result;
        }
        
        public bool Player_SetEqualiserEnabled(bool enabled)
        {
            var response = _client.Player_SetEqualiserEnabled(new Player_SetEqualiserEnabled_Request
            {
                Enabled = enabled,
            });
            return response.Result;
        }
        
        public bool Player_GetDspEnabled()
        {
            var response = _client.Player_GetDspEnabled(new Empty());
            return response.Result;
        }
        
        public bool Player_SetDspEnabled(bool enabled)
        {
            var response = _client.Player_SetDspEnabled(new Player_SetDspEnabled_Request
            {
                Enabled = enabled,
            });
            return response.Result;
        }
        
        public bool Player_GetScrobbleEnabled()
        {
            var response = _client.Player_GetScrobbleEnabled(new Empty());
            return response.Result;
        }
        
        public bool Player_SetScrobbleEnabled(bool enabled)
        {
            var response = _client.Player_SetScrobbleEnabled(new Player_SetScrobbleEnabled_Request
            {
                Enabled = enabled,
            });
            return response.Result;
        }
        
        public string NowPlaying_GetFileUrl()
        {
            var response = _client.NowPlaying_GetFileUrl(new Empty());
            return response.Result;
        }
        
        public int NowPlaying_GetDuration()
        {
            var response = _client.NowPlaying_GetDuration(new Empty());
            return response.Result;
        }
        
        public string NowPlaying_GetFileProperty(FilePropertyType type)
        {
            var response = _client.NowPlaying_GetFileProperty(new NowPlaying_GetFileProperty_Request
            {
                Type = (int)type,
            });
            return response.Result;
        }
        
        public string NowPlaying_GetFileTag(MetaDataType field)
        {
            var response = _client.NowPlaying_GetFileTag(new NowPlaying_GetFileTag_Request
            {
                Field = (int)field,
            });
            return response.Result;
        }
        
        public string NowPlaying_GetLyrics()
        {
            var response = _client.NowPlaying_GetLyrics(new Empty());
            return response.Result;
        }
        
        public string NowPlaying_GetArtwork()
        {
            var response = _client.NowPlaying_GetArtwork(new Empty());
            return response.Result;
        }
        
        public bool NowPlayingList_Clear()
        {
            var response = _client.NowPlayingList_Clear(new Empty());
            return response.Result;
        }
        
        public bool NowPlayingList_QueryFiles(string query)
        {
            var response = _client.NowPlayingList_QueryFiles(new NowPlayingList_QueryFiles_Request
            {
                Query = query,
            });
            return response.Result;
        }
        
        public string NowPlayingList_QueryGetNextFile()
        {
            var response = _client.NowPlayingList_QueryGetNextFile(new Empty());
            return response.Result;
        }
        
        public bool NowPlayingList_PlayNow(string sourceFileUrl)
        {
            var response = _client.NowPlayingList_PlayNow(new NowPlayingList_PlayNow_Request
            {
                SourceFileUrl = sourceFileUrl,
            });
            return response.Result;
        }
        
        public bool NowPlayingList_QueueNext(string sourceFileUrl)
        {
            var response = _client.NowPlayingList_QueueNext(new NowPlayingList_QueueNext_Request
            {
                SourceFileUrl = sourceFileUrl,
            });
            return response.Result;
        }
        
        public bool NowPlayingList_QueueLast(string sourceFileUrl)
        {
            var response = _client.NowPlayingList_QueueLast(new NowPlayingList_QueueLast_Request
            {
                SourceFileUrl = sourceFileUrl,
            });
            return response.Result;
        }
        
        public bool NowPlayingList_PlayLibraryShuffled()
        {
            var response = _client.NowPlayingList_PlayLibraryShuffled(new Empty());
            return response.Result;
        }
        
        public bool Playlist_QueryPlaylists()
        {
            var response = _client.Playlist_QueryPlaylists(new Empty());
            return response.Result;
        }
        
        public string Playlist_QueryGetNextPlaylist()
        {
            var response = _client.Playlist_QueryGetNextPlaylist(new Empty());
            return response.Result;
        }
        
        public PlaylistFormat Playlist_GetType(string playlistUrl)
        {
            var response = _client.Playlist_GetType(new Playlist_GetType_Request
            {
                PlaylistUrl = playlistUrl,
            });
            return (PlaylistFormat)response.Result;
        }
        
        public bool Playlist_QueryFiles(string playlistUrl)
        {
            var response = _client.Playlist_QueryFiles(new Playlist_QueryFiles_Request
            {
                PlaylistUrl = playlistUrl,
            });
            return response.Result;
        }
        
        public string Playlist_QueryGetNextFile()
        {
            var response = _client.Playlist_QueryGetNextFile(new Empty());
            return response.Result;
        }
        
        public void MB_RefreshPanels()
        {
            _client.MB_RefreshPanels(new Empty());
        }
        
        public void MB_SendNotification(CallbackType type)
        {
            _client.MB_SendNotification(new MB_SendNotification_Request
            {
                Type = (int)type,
            });
        }
        
        public string Setting_GetFieldName(MetaDataType field)
        {
            var response = _client.Setting_GetFieldName(new Setting_GetFieldName_Request
            {
                Field = (int)field,
            });
            return response.Result;
        }
        
        public void MB_SetBackgroundTaskMessage(string message)
        {
            _client.MB_SetBackgroundTaskMessage(new MB_SetBackgroundTaskMessage_Request
            {
                Message = message,
            });
        }
        
        public bool Player_GetShowTimeRemaining()
        {
            var response = _client.Player_GetShowTimeRemaining(new Empty());
            return response.Result;
        }
        
        public int NowPlayingList_GetCurrentIndex()
        {
            var response = _client.NowPlayingList_GetCurrentIndex(new Empty());
            return response.Result;
        }
        
        public string NowPlayingList_GetListFileUrl(int index)
        {
            var response = _client.NowPlayingList_GetListFileUrl(new NowPlayingList_GetListFileUrl_Request
            {
                Index = index,
            });
            return response.Result;
        }
        
        public string NowPlayingList_GetFileProperty(int index, FilePropertyType type)
        {
            var response = _client.NowPlayingList_GetFileProperty(new NowPlayingList_GetFileProperty_Request
            {
                Index = index,
                Type = (int)type,
            });
            return response.Result;
        }
        
        public string NowPlayingList_GetFileTag(int index, MetaDataType field)
        {
            var response = _client.NowPlayingList_GetFileTag(new NowPlayingList_GetFileTag_Request
            {
                Index = index,
                Field = (int)field,
            });
            return response.Result;
        }
        
        public int NowPlaying_GetSpectrumData(float[] fftData)
        {
            var response = _client.NowPlaying_GetSpectrumData(new NowPlaying_GetSpectrumData_Request
            {
                FftData = { fftData },
            });
            return response.Result;
        }
        
        public bool NowPlaying_GetSoundGraph(float[] graphData)
        {
            var response = _client.NowPlaying_GetSoundGraph(new NowPlaying_GetSoundGraph_Request
            {
                GraphData = { graphData },
            });
            return response.Result;
        }
        
        public string MB_GetLocalisation(string id, string defaultText)
        {
            var response = _client.MB_GetLocalisation(new MB_GetLocalisation_Request
            {
                Id = id,
                DefaultText = defaultText,
            });
            return response.Result;
        }
        
        public bool NowPlayingList_IsAnyPriorTracks()
        {
            var response = _client.NowPlayingList_IsAnyPriorTracks(new Empty());
            return response.Result;
        }
        
        public bool Player_ShowEqualiser()
        {
            var response = _client.Player_ShowEqualiser(new Empty());
            return response.Result;
        }
        
        public bool Player_GetAutoDjEnabled()
        {
            var response = _client.Player_GetAutoDjEnabled(new Empty());
            return response.Result;
        }
        
        public bool Player_GetStopAfterCurrentEnabled()
        {
            var response = _client.Player_GetStopAfterCurrentEnabled(new Empty());
            return response.Result;
        }
        
        public bool Player_GetCrossfade()
        {
            var response = _client.Player_GetCrossfade(new Empty());
            return response.Result;
        }
        
        public bool Player_SetCrossfade(bool crossfade)
        {
            var response = _client.Player_SetCrossfade(new Player_SetCrossfade_Request
            {
                Crossfade = crossfade,
            });
            return response.Result;
        }
        
        public ReplayGainMode Player_GetReplayGainMode()
        {
            var response = _client.Player_GetReplayGainMode(new Empty());
            return (ReplayGainMode)response.Result;
        }
        
        public bool Player_SetReplayGainMode(ReplayGainMode mode)
        {
            var response = _client.Player_SetReplayGainMode(new Player_SetReplayGainMode_Request
            {
                Mode = (int)mode,
            });
            return response.Result;
        }
        
        public int Player_QueueRandomTracks(int count)
        {
            var response = _client.Player_QueueRandomTracks(new Player_QueueRandomTracks_Request
            {
                Count = count,
            });
            return response.Result;
        }
        
        public DataType Setting_GetDataType(MetaDataType field)
        {
            var response = _client.Setting_GetDataType(new Setting_GetDataType_Request
            {
                Field = (int)field,
            });
            return (DataType)response.Result;
        }
        
        public int NowPlayingList_GetNextIndex(int offset)
        {
            var response = _client.NowPlayingList_GetNextIndex(new NowPlayingList_GetNextIndex_Request
            {
                Offset = offset,
            });
            return response.Result;
        }
        
        public string NowPlaying_GetArtistPicture(int fadingPercent)
        {
            var response = _client.NowPlaying_GetArtistPicture(new NowPlaying_GetArtistPicture_Request
            {
                FadingPercent = fadingPercent,
            });
            return response.Result;
        }
        
        public string NowPlaying_GetDownloadedArtwork()
        {
            var response = _client.NowPlaying_GetDownloadedArtwork(new Empty());
            return response.Result;
        }
        
        public bool MB_ShowNowPlayingAssistant()
        {
            var response = _client.MB_ShowNowPlayingAssistant(new Empty());
            return response.Result;
        }
        
        public string NowPlaying_GetDownloadedLyrics()
        {
            var response = _client.NowPlaying_GetDownloadedLyrics(new Empty());
            return response.Result;
        }
        
        public bool Player_GetShowRatingTrack()
        {
            var response = _client.Player_GetShowRatingTrack(new Empty());
            return response.Result;
        }
        
        public bool Player_GetShowRatingLove()
        {
            var response = _client.Player_GetShowRatingLove(new Empty());
            return response.Result;
        }
        
        public string Setting_GetLastFmUserId()
        {
            var response = _client.Setting_GetLastFmUserId(new Empty());
            return response.Result;
        }
        
        public string Playlist_GetName(string playlistUrl)
        {
            var response = _client.Playlist_GetName(new Playlist_GetName_Request
            {
                PlaylistUrl = playlistUrl,
            });
            return response.Result;
        }
        
        public string Playlist_CreatePlaylist(string folderName, string playlistName, string[] filenames)
        {
            var response = _client.Playlist_CreatePlaylist(new Playlist_CreatePlaylist_Request
            {
                FolderName = folderName,
                PlaylistName = playlistName,
                Filenames = { filenames },
            });
            return response.Result;
        }
        
        public bool Playlist_SetFiles(string playlistUrl, string[] filenames)
        {
            var response = _client.Playlist_SetFiles(new Playlist_SetFiles_Request
            {
                PlaylistUrl = playlistUrl,
                Filenames = { filenames },
            });
            return response.Result;
        }
        
        public string Library_QuerySimilarArtists(string artistName, double minimumArtistSimilarityRating)
        {
            var response = _client.Library_QuerySimilarArtists(new Library_QuerySimilarArtists_Request
            {
                ArtistName = artistName,
                MinimumArtistSimilarityRating = minimumArtistSimilarityRating,
            });
            return response.Result;
        }
        
        public bool Library_QueryLookupTable(string keyTags, string valueTags, string query)
        {
            var response = _client.Library_QueryLookupTable(new Library_QueryLookupTable_Request
            {
                KeyTags = keyTags,
                ValueTags = valueTags,
                Query = query,
            });
            return response.Result;
        }
        
        public string Library_QueryGetLookupTableValue(string key)
        {
            var response = _client.Library_QueryGetLookupTableValue(new Library_QueryGetLookupTableValue_Request
            {
                Key = key,
            });
            return response.Result;
        }
        
        public bool NowPlayingList_QueueFilesNext(string[] sourceFileUrl)
        {
            var response = _client.NowPlayingList_QueueFilesNext(new NowPlayingList_QueueFilesNext_Request
            {
                SourceFileUrl = { sourceFileUrl },
            });
            return response.Result;
        }
        
        public bool NowPlayingList_QueueFilesLast(string[] sourceFileUrl)
        {
            var response = _client.NowPlayingList_QueueFilesLast(new NowPlayingList_QueueFilesLast_Request
            {
                SourceFileUrl = { sourceFileUrl },
            });
            return response.Result;
        }
        
        public string Setting_GetWebProxy()
        {
            var response = _client.Setting_GetWebProxy(new Empty());
            return response.Result;
        }
        
        public bool NowPlayingList_RemoveAt(int index)
        {
            var response = _client.NowPlayingList_RemoveAt(new NowPlayingList_RemoveAt_Request
            {
                Index = index,
            });
            return response.Result;
        }
        
        public bool Playlist_RemoveAt(string playlistUrl, int index)
        {
            var response = _client.Playlist_RemoveAt(new Playlist_RemoveAt_Request
            {
                PlaylistUrl = playlistUrl,
                Index = index,
            });
            return response.Result;
        }
        
        public bool MB_OpenFilterInTab(MetaDataType field1, ComparisonType comparison1, string value1, MetaDataType field2, ComparisonType comparison2, string value2)
        {
            var response = _client.MB_OpenFilterInTab(new MB_OpenFilterInTab_Request
            {
                Field1 = (int)field1,
                Comparison1 = (int)comparison1,
                Value1 = value1,
                Field2 = (int)field2,
                Comparison2 = (int)comparison2,
                Value2 = value2,
            });
            return response.Result;
        }
        
        public bool MB_SetWindowSize(int width, int height)
        {
            var response = _client.MB_SetWindowSize(new MB_SetWindowSize_Request
            {
                Width = width,
                Height = height,
            });
            return response.Result;
        }
        
        public string Library_GetArtistPicture(string artistName, int fadingPercent, int fadingColor)
        {
            var response = _client.Library_GetArtistPicture(new Library_GetArtistPicture_Request
            {
                ArtistName = artistName,
                FadingPercent = fadingPercent,
                FadingColor = fadingColor,
            });
            return response.Result;
        }
        
        public string Pending_GetFileUrl()
        {
            var response = _client.Pending_GetFileUrl(new Empty());
            return response.Result;
        }
        
        public string Pending_GetFileProperty(FilePropertyType field)
        {
            var response = _client.Pending_GetFileProperty(new Pending_GetFileProperty_Request
            {
                Field = (int)field,
            });
            return response.Result;
        }
        
        public string Pending_GetFileTag(MetaDataType field)
        {
            var response = _client.Pending_GetFileTag(new Pending_GetFileTag_Request
            {
                Field = (int)field,
            });
            return response.Result;
        }
        
        public bool Player_GetButtonEnabled(PlayButtonType button)
        {
            var response = _client.Player_GetButtonEnabled(new Player_GetButtonEnabled_Request
            {
                Button = (int)button,
            });
            return response.Result;
        }
        
        public bool NowPlayingList_MoveFiles(int[] fromIndices, int toIndex)
        {
            var response = _client.NowPlayingList_MoveFiles(new NowPlayingList_MoveFiles_Request
            {
                FromIndices = { fromIndices },
                ToIndex = toIndex,
            });
            return response.Result;
        }
        
        public string Library_GetArtworkUrl(string sourceFileUrl, int index)
        {
            var response = _client.Library_GetArtworkUrl(new Library_GetArtworkUrl_Request
            {
                SourceFileUrl = sourceFileUrl,
                Index = index,
            });
            return response.Result;
        }
        
        public string Library_GetArtistPictureThumb(string artistName)
        {
            var response = _client.Library_GetArtistPictureThumb(new Library_GetArtistPictureThumb_Request
            {
                ArtistName = artistName,
            });
            return response.Result;
        }
        
        public string NowPlaying_GetArtworkUrl()
        {
            var response = _client.NowPlaying_GetArtworkUrl(new Empty());
            return response.Result;
        }
        
        public string NowPlaying_GetDownloadedArtworkUrl()
        {
            var response = _client.NowPlaying_GetDownloadedArtworkUrl(new Empty());
            return response.Result;
        }
        
        public string NowPlaying_GetArtistPictureThumb()
        {
            var response = _client.NowPlaying_GetArtistPictureThumb(new Empty());
            return response.Result;
        }
        
        public bool Playlist_IsInList(string playlistUrl, string filename)
        {
            var response = _client.Playlist_IsInList(new Playlist_IsInList_Request
            {
                PlaylistUrl = playlistUrl,
                Filename = filename,
            });
            return response.Result;
        }
        
        public bool Library_GetArtistPictureUrls(string artistName, bool localOnly, out string[] urls)
        {
            var response = _client.Library_GetArtistPictureUrls(new Library_GetArtistPictureUrls_Request
            {
                ArtistName = artistName,
                LocalOnly = localOnly,
            });
            urls = response.Urls.ToArray();
            return response.Result;
        }
        
        public bool NowPlaying_GetArtistPictureUrls(bool localOnly, out string[] urls)
        {
            var response = _client.NowPlaying_GetArtistPictureUrls(new NowPlaying_GetArtistPictureUrls_Request
            {
                LocalOnly = localOnly,
            });
            urls = response.Urls.ToArray();
            return response.Result;
        }
        
        public bool Playlist_AppendFiles(string playlistUrl, string[] filenames)
        {
            var response = _client.Playlist_AppendFiles(new Playlist_AppendFiles_Request
            {
                PlaylistUrl = playlistUrl,
                Filenames = { filenames },
            });
            return response.Result;
        }
        
        public string Sync_FileStart(string filename)
        {
            var response = _client.Sync_FileStart(new Sync_FileStart_Request
            {
                Filename = filename,
            });
            return response.Result;
        }
        
        public void Sync_FileEnd(string filename, bool success, string errorMessage)
        {
            _client.Sync_FileEnd(new Sync_FileEnd_Request
            {
                Filename = filename,
                Success = success,
                ErrorMessage = errorMessage,
            });
        }
        
        public bool Library_QueryFilesEx(string query, out string[]? files)
        {
            var response = _client.Library_QueryFilesEx(new Library_QueryFilesEx_Request
            {
                Query = query,
            });
            files = response.Files.ToArray();
            return response.Result;
        }
        
        public bool NowPlayingList_QueryFilesEx(string query, out string[]? files)
        {
            var response = _client.NowPlayingList_QueryFilesEx(new NowPlayingList_QueryFilesEx_Request
            {
                Query = query,
            });
            files = response.Files.ToArray();
            return response.Result;
        }
        
        public bool Playlist_QueryFilesEx(string playlistUrl, out string[] filenames)
        {
            var response = _client.Playlist_QueryFilesEx(new Playlist_QueryFilesEx_Request
            {
                PlaylistUrl = playlistUrl,
            });
            filenames = response.Filenames.ToArray();
            return response.Result;
        }
        
        public bool Playlist_MoveFiles(string playlistUrl, int[] fromIndices, int toIndex)
        {
            var response = _client.Playlist_MoveFiles(new Playlist_MoveFiles_Request
            {
                PlaylistUrl = playlistUrl,
                FromIndices = { fromIndices },
                ToIndex = toIndex,
            });
            return response.Result;
        }
        
        public bool Playlist_PlayNow(string playlistUrl)
        {
            var response = _client.Playlist_PlayNow(new Playlist_PlayNow_Request
            {
                PlaylistUrl = playlistUrl,
            });
            return response.Result;
        }
        
        public bool NowPlaying_IsSoundtrack()
        {
            var response = _client.NowPlaying_IsSoundtrack(new Empty());
            return response.Result;
        }
        
        public bool NowPlaying_GetSoundtrackPictureUrls(bool localOnly, out string[] urls)
        {
            var response = _client.NowPlaying_GetSoundtrackPictureUrls(new NowPlaying_GetSoundtrackPictureUrls_Request
            {
                LocalOnly = localOnly,
            });
            urls = response.Urls.ToArray();
            return response.Result;
        }
        
        public string Library_GetDevicePersistentId(string sourceFileUrl, DeviceIdType idType)
        {
            var response = _client.Library_GetDevicePersistentId(new Library_GetDevicePersistentId_Request
            {
                SourceFileUrl = sourceFileUrl,
                IdType = (int)idType,
            });
            return response.Result;
        }
        
        public bool Library_SetDevicePersistentId(string sourceFileUrl, DeviceIdType idType, string value)
        {
            var response = _client.Library_SetDevicePersistentId(new Library_SetDevicePersistentId_Request
            {
                SourceFileUrl = sourceFileUrl,
                IdType = (int)idType,
                Value = value,
            });
            return response.Result;
        }
        
        public bool Library_FindDevicePersistentId(DeviceIdType idType, string[] ids, out string[] values)
        {
            var response = _client.Library_FindDevicePersistentId(new Library_FindDevicePersistentId_Request
            {
                IdType = (int)idType,
                Ids = { ids },
            });
            values = response.Values.ToArray();
            return response.Result;
        }
        
        public string Library_AddFileToLibrary(string sourceFileUrl, LibraryCategory category)
        {
            var response = _client.Library_AddFileToLibrary(new Library_AddFileToLibrary_Request
            {
                SourceFileUrl = sourceFileUrl,
                Category = (int)category,
            });
            return response.Result;
        }
        
        public bool Playlist_DeletePlaylist(string playlistUrl)
        {
            var response = _client.Playlist_DeletePlaylist(new Playlist_DeletePlaylist_Request
            {
                PlaylistUrl = playlistUrl,
            });
            return response.Result;
        }
        
        public bool Library_GetFileTags(string sourceFileUrl, MetaDataType[] fields, out string[] results)
        {
            var response = _client.Library_GetFileTags(new Library_GetFileTags_Request
            {
                SourceFileUrl = sourceFileUrl,
                Fields = { fields.Cast<int>() },
            });
            results = response.Results.ToArray();
            return response.Result;
        }
        
        public bool NowPlaying_GetFileTags(MetaDataType[] fields, out string[] results)
        {
            var response = _client.NowPlaying_GetFileTags(new NowPlaying_GetFileTags_Request
            {
                Fields = { fields.Cast<int>() },
            });
            results = response.Results.ToArray();
            return response.Result;
        }
        
        public bool NowPlayingList_GetFileTags(int index, MetaDataType[] fields, out string[] results)
        {
            var response = _client.NowPlayingList_GetFileTags(new NowPlayingList_GetFileTags_Request
            {
                Index = index,
                Fields = { fields.Cast<int>() },
            });
            results = response.Results.ToArray();
            return response.Result;
        }
        
        public bool MB_DownloadFile(string url, DownloadTarget target, string targetFolder, bool cancelDownload)
        {
            var response = _client.MB_DownloadFile(new MB_DownloadFile_Request
            {
                Url = url,
                Target = (int)target,
                TargetFolder = targetFolder,
                CancelDownload = cancelDownload,
            });
            return response.Result;
        }
        
        public string Setting_GetFileConvertCommandLine(FileCodec codec, EncodeQuality encodeQuality)
        {
            var response = _client.Setting_GetFileConvertCommandLine(new Setting_GetFileConvertCommandLine_Request
            {
                Codec = (int)codec,
                EncodeQuality = (int)encodeQuality,
            });
            return response.Result;
        }
        
        public int Player_OpenStreamHandle(string url, bool useMusicBeeSettings, bool enableDsp, ReplayGainMode gainType)
        {
            var response = _client.Player_OpenStreamHandle(new Player_OpenStreamHandle_Request
            {
                Url = url,
                UseMusicBeeSettings = useMusicBeeSettings,
                EnableDsp = enableDsp,
                GainType = (int)gainType,
            });
            return response.Result;
        }
        
        public bool Player_UpdatePlayStatistics(string url, PlayStatisticType countType, bool disableScrobble)
        {
            var response = _client.Player_UpdatePlayStatistics(new Player_UpdatePlayStatistics_Request
            {
                Url = url,
                CountType = (int)countType,
                DisableScrobble = disableScrobble,
            });
            return response.Result;
        }
        
        public bool Library_GetArtworkEx(string sourceFileUrl, int index, bool retrievePictureData, out PictureLocations pictureLocations, out string pictureUrl, out byte[] imageData)
        {
            var response = _client.Library_GetArtworkEx(new Library_GetArtworkEx_Request
            {
                SourceFileUrl = sourceFileUrl,
                Index = index,
                RetrievePictureData = retrievePictureData,
            });
            pictureLocations = (PictureLocations)response.PictureLocations;
            pictureUrl = response.PictureUrl;
            imageData = response.ImageData.ToByteArray();
            return response.Result;
        }
        
        public bool Library_SetArtworkEx(string sourceFileUrl, int index, byte[] imageData)
        {
            var response = _client.Library_SetArtworkEx(new Library_SetArtworkEx_Request
            {
                SourceFileUrl = sourceFileUrl,
                Index = index,
                ImageData = ByteString.CopyFrom(imageData),
            });
            return response.Result;
        }
        
        public bool MB_GetVisualiserInformation(out string[] visualiserNames, out string defaultVisualiserName, out WindowState defaultState, out WindowState currentState)
        {
            var response = _client.MB_GetVisualiserInformation(new Empty());
            visualiserNames = response.VisualiserNames.ToArray();
            defaultVisualiserName = response.DefaultVisualiserName;
            defaultState = (WindowState)response.DefaultState;
            currentState = (WindowState)response.CurrentState;
            return response.Result;
        }
        
        public bool MB_ShowVisualiser(string visualiserName, WindowState state)
        {
            var response = _client.MB_ShowVisualiser(new MB_ShowVisualiser_Request
            {
                VisualiserName = visualiserName,
                State = (int)state,
            });
            return response.Result;
        }
        
        public bool MB_GetPluginViewInformation(string pluginFilename, out string[] viewNames, out string defaultViewName, out WindowState defaultState, out WindowState currentState)
        {
            var response = _client.MB_GetPluginViewInformation(new MB_GetPluginViewInformation_Request
            {
                PluginFilename = pluginFilename,
            });
            viewNames = response.ViewNames.ToArray();
            defaultViewName = response.DefaultViewName;
            defaultState = (WindowState)response.DefaultState;
            currentState = (WindowState)response.CurrentState;
            return response.Result;
        }
        
        public bool MB_ShowPluginView(string pluginFilename, string viewName, WindowState state)
        {
            var response = _client.MB_ShowPluginView(new MB_ShowPluginView_Request
            {
                PluginFilename = pluginFilename,
                ViewName = viewName,
                State = (int)state,
            });
            return response.Result;
        }
        
        public bool Player_GetOutputDevices(out string[] deviceNames, out string activeDeviceName)
        {
            var response = _client.Player_GetOutputDevices(new Empty());
            deviceNames = response.DeviceNames.ToArray();
            activeDeviceName = response.ActiveDeviceName;
            return response.Result;
        }
        
        public bool Player_SetOutputDevice(string deviceName)
        {
            var response = _client.Player_SetOutputDevice(new Player_SetOutputDevice_Request
            {
                DeviceName = deviceName,
            });
            return response.Result;
        }
        
        public bool MB_UninstallPlugin(string pluginFilename, string password)
        {
            var response = _client.MB_UninstallPlugin(new MB_UninstallPlugin_Request
            {
                PluginFilename = pluginFilename,
                Password = password,
            });
            return response.Result;
        }
        
        public bool Player_PlayPreviousAlbum()
        {
            var response = _client.Player_PlayPreviousAlbum(new Empty());
            return response.Result;
        }
        
        public bool Player_PlayNextAlbum()
        {
            var response = _client.Player_PlayNextAlbum(new Empty());
            return response.Result;
        }
        
        public bool Podcasts_QuerySubscriptions(string query, out string[] ids)
        {
            var response = _client.Podcasts_QuerySubscriptions(new Podcasts_QuerySubscriptions_Request
            {
                Query = query,
            });
            ids = response.Ids.ToArray();
            return response.Result;
        }
        
        public bool Podcasts_GetSubscription(string id, out string[] subscription)
        {
            var response = _client.Podcasts_GetSubscription(new Podcasts_GetSubscription_Request
            {
                Id = id,
            });
            subscription = response.Subscription.ToArray();
            return response.Result;
        }
        
        public bool Podcasts_GetSubscriptionArtwork(string id, int index, out byte[] imageData)
        {
            var response = _client.Podcasts_GetSubscriptionArtwork(new Podcasts_GetSubscriptionArtwork_Request
            {
                Id = id,
                Index = index,
            });
            imageData = response.ImageData.ToByteArray();
            return response.Result;
        }
        
        public bool Podcasts_GetSubscriptionEpisodes(string id, out string[] urls)
        {
            var response = _client.Podcasts_GetSubscriptionEpisodes(new Podcasts_GetSubscriptionEpisodes_Request
            {
                Id = id,
            });
            urls = response.Urls.ToArray();
            return response.Result;
        }
        
        public bool Podcasts_GetSubscriptionEpisode(string id, int index, out string[] episode)
        {
            var response = _client.Podcasts_GetSubscriptionEpisode(new Podcasts_GetSubscriptionEpisode_Request
            {
                Id = id,
                Index = index,
            });
            episode = response.Episode.ToArray();
            return response.Result;
        }
        
        public bool NowPlaying_GetSoundGraphEx(float[] graphData, float[] peakData)
        {
            var response = _client.NowPlaying_GetSoundGraphEx(new NowPlaying_GetSoundGraphEx_Request
            {
                GraphData = { graphData },
                PeakData = { peakData },
            });
            return response.Result;
        }
        
        public string Sync_FileDeleteStart(string filename)
        {
            var response = _client.Sync_FileDeleteStart(new Sync_FileDeleteStart_Request
            {
                Filename = filename,
            });
            return response.Result;
        }
        
        public void Sync_FileDeleteEnd(string filename, bool success, string errorMessage)
        {
            _client.Sync_FileDeleteEnd(new Sync_FileDeleteEnd_Request
            {
                Filename = filename,
                Success = success,
                ErrorMessage = errorMessage,
            });
        }
        
    }
}
