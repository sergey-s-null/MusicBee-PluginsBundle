using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;

// ReSharper disable ConstantNullCoalescingCondition

namespace Module.RemoteMusicBeeApi
{
    public class MusicBeeApiServiceImpl : MusicBeeApiService.MusicBeeApiServiceBase
    {
        private readonly IBaseMusicBeeApi _mbApi;
        
        public MusicBeeApiServiceImpl(IBaseMusicBeeApi mbApi)
        {
            _mbApi = mbApi;
        }
        
        public override Task<Empty> MB_ReleaseString(MB_ReleaseString_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                _mbApi.MB_ReleaseString(request.P1);
                return new Empty();
            });
        }
        
        public override Task<Empty> MB_Trace(MB_Trace_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                _mbApi.MB_Trace(request.P1);
                return new Empty();
            });
        }
        
        public override Task<Setting_GetPersistentStoragePath_Response> Setting_GetPersistentStoragePath(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Setting_GetPersistentStoragePath();
                return new Setting_GetPersistentStoragePath_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Setting_GetSkin_Response> Setting_GetSkin(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Setting_GetSkin();
                return new Setting_GetSkin_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Setting_GetSkinElementColour_Response> Setting_GetSkinElementColour(Setting_GetSkinElementColour_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Setting_GetSkinElementColour((SkinElement)request.Element, (ElementState)request.State, (ElementComponent)request.Component);
                return new Setting_GetSkinElementColour_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Setting_IsWindowBordersSkinned_Response> Setting_IsWindowBordersSkinned(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Setting_IsWindowBordersSkinned();
                return new Setting_IsWindowBordersSkinned_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_GetFileProperty_Response> Library_GetFileProperty(Library_GetFileProperty_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetFileProperty(request.SourceFileUrl, (FilePropertyType)request.Type);
                return new Library_GetFileProperty_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_GetFileTag_Response> Library_GetFileTag(Library_GetFileTag_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetFileTag(request.SourceFileUrl, (MetaDataType)request.Field);
                return new Library_GetFileTag_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_SetFileTag_Response> Library_SetFileTag(Library_SetFileTag_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_SetFileTag(request.SourceFileUrl, (MetaDataType)request.Field, request.Value);
                return new Library_SetFileTag_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_CommitTagsToFile_Response> Library_CommitTagsToFile(Library_CommitTagsToFile_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_CommitTagsToFile(request.SourceFileUrl);
                return new Library_CommitTagsToFile_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_GetLyrics_Response> Library_GetLyrics(Library_GetLyrics_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetLyrics(request.SourceFileUrl, (LyricsType)request.Type);
                return new Library_GetLyrics_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_QueryFiles_Response> Library_QueryFiles(Library_QueryFiles_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_QueryFiles(request.Query);
                return new Library_QueryFiles_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_QueryGetNextFile_Response> Library_QueryGetNextFile(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_QueryGetNextFile();
                return new Library_QueryGetNextFile_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetPosition_Response> Player_GetPosition(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetPosition();
                return new Player_GetPosition_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_SetPosition_Response> Player_SetPosition(Player_SetPosition_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetPosition(request.Position);
                return new Player_SetPosition_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetPlayState_Response> Player_GetPlayState(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetPlayState();
                return new Player_GetPlayState_Response()
                {
                    Result = (int)result,
                };
            });
        }
        
        public override Task<Player_PlayPause_Response> Player_PlayPause(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_PlayPause();
                return new Player_PlayPause_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_Stop_Response> Player_Stop(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_Stop();
                return new Player_Stop_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_StopAfterCurrent_Response> Player_StopAfterCurrent(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_StopAfterCurrent();
                return new Player_StopAfterCurrent_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_PlayPreviousTrack_Response> Player_PlayPreviousTrack(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_PlayPreviousTrack();
                return new Player_PlayPreviousTrack_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_PlayNextTrack_Response> Player_PlayNextTrack(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_PlayNextTrack();
                return new Player_PlayNextTrack_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_StartAutoDj_Response> Player_StartAutoDj(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_StartAutoDj();
                return new Player_StartAutoDj_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_EndAutoDj_Response> Player_EndAutoDj(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_EndAutoDj();
                return new Player_EndAutoDj_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetVolume_Response> Player_GetVolume(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetVolume();
                return new Player_GetVolume_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_SetVolume_Response> Player_SetVolume(Player_SetVolume_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetVolume(request.Volume);
                return new Player_SetVolume_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetMute_Response> Player_GetMute(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetMute();
                return new Player_GetMute_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_SetMute_Response> Player_SetMute(Player_SetMute_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetMute(request.Mute);
                return new Player_SetMute_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetShuffle_Response> Player_GetShuffle(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetShuffle();
                return new Player_GetShuffle_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_SetShuffle_Response> Player_SetShuffle(Player_SetShuffle_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetShuffle(request.Shuffle);
                return new Player_SetShuffle_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetRepeat_Response> Player_GetRepeat(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetRepeat();
                return new Player_GetRepeat_Response()
                {
                    Result = (int)result,
                };
            });
        }
        
        public override Task<Player_SetRepeat_Response> Player_SetRepeat(Player_SetRepeat_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetRepeat((RepeatMode)request.Repeat);
                return new Player_SetRepeat_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetEqualiserEnabled_Response> Player_GetEqualiserEnabled(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetEqualiserEnabled();
                return new Player_GetEqualiserEnabled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_SetEqualiserEnabled_Response> Player_SetEqualiserEnabled(Player_SetEqualiserEnabled_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetEqualiserEnabled(request.Enabled);
                return new Player_SetEqualiserEnabled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetDspEnabled_Response> Player_GetDspEnabled(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetDspEnabled();
                return new Player_GetDspEnabled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_SetDspEnabled_Response> Player_SetDspEnabled(Player_SetDspEnabled_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetDspEnabled(request.Enabled);
                return new Player_SetDspEnabled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetScrobbleEnabled_Response> Player_GetScrobbleEnabled(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetScrobbleEnabled();
                return new Player_GetScrobbleEnabled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_SetScrobbleEnabled_Response> Player_SetScrobbleEnabled(Player_SetScrobbleEnabled_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetScrobbleEnabled(request.Enabled);
                return new Player_SetScrobbleEnabled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetFileUrl_Response> NowPlaying_GetFileUrl(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetFileUrl();
                return new NowPlaying_GetFileUrl_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetDuration_Response> NowPlaying_GetDuration(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetDuration();
                return new NowPlaying_GetDuration_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetFileProperty_Response> NowPlaying_GetFileProperty(NowPlaying_GetFileProperty_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetFileProperty((FilePropertyType)request.Type);
                return new NowPlaying_GetFileProperty_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetFileTag_Response> NowPlaying_GetFileTag(NowPlaying_GetFileTag_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetFileTag((MetaDataType)request.Field);
                return new NowPlaying_GetFileTag_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetLyrics_Response> NowPlaying_GetLyrics(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetLyrics();
                return new NowPlaying_GetLyrics_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetArtwork_Response> NowPlaying_GetArtwork(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetArtwork();
                return new NowPlaying_GetArtwork_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_Clear_Response> NowPlayingList_Clear(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_Clear();
                return new NowPlayingList_Clear_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_QueryFiles_Response> NowPlayingList_QueryFiles(NowPlayingList_QueryFiles_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_QueryFiles(request.Query);
                return new NowPlayingList_QueryFiles_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_QueryGetNextFile_Response> NowPlayingList_QueryGetNextFile(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_QueryGetNextFile();
                return new NowPlayingList_QueryGetNextFile_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_PlayNow_Response> NowPlayingList_PlayNow(NowPlayingList_PlayNow_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_PlayNow(request.SourceFileUrl);
                return new NowPlayingList_PlayNow_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_QueueNext_Response> NowPlayingList_QueueNext(NowPlayingList_QueueNext_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_QueueNext(request.SourceFileUrl);
                return new NowPlayingList_QueueNext_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_QueueLast_Response> NowPlayingList_QueueLast(NowPlayingList_QueueLast_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_QueueLast(request.SourceFileUrl);
                return new NowPlayingList_QueueLast_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_PlayLibraryShuffled_Response> NowPlayingList_PlayLibraryShuffled(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_PlayLibraryShuffled();
                return new NowPlayingList_PlayLibraryShuffled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_QueryPlaylists_Response> Playlist_QueryPlaylists(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_QueryPlaylists();
                return new Playlist_QueryPlaylists_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_QueryGetNextPlaylist_Response> Playlist_QueryGetNextPlaylist(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_QueryGetNextPlaylist();
                return new Playlist_QueryGetNextPlaylist_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_GetType_Response> Playlist_GetType(Playlist_GetType_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_GetType(request.PlaylistUrl);
                return new Playlist_GetType_Response()
                {
                    Result = (int)result,
                };
            });
        }
        
        public override Task<Playlist_QueryFiles_Response> Playlist_QueryFiles(Playlist_QueryFiles_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_QueryFiles(request.PlaylistUrl);
                return new Playlist_QueryFiles_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_QueryGetNextFile_Response> Playlist_QueryGetNextFile(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_QueryGetNextFile();
                return new Playlist_QueryGetNextFile_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Empty> MB_RefreshPanels(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                _mbApi.MB_RefreshPanels();
                return new Empty();
            });
        }
        
        public override Task<Empty> MB_SendNotification(MB_SendNotification_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                _mbApi.MB_SendNotification((CallbackType)request.Type);
                return new Empty();
            });
        }
        
        public override Task<Setting_GetFieldName_Response> Setting_GetFieldName(Setting_GetFieldName_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Setting_GetFieldName((MetaDataType)request.Field);
                return new Setting_GetFieldName_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Empty> MB_SetBackgroundTaskMessage(MB_SetBackgroundTaskMessage_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                _mbApi.MB_SetBackgroundTaskMessage(request.Message);
                return new Empty();
            });
        }
        
        public override Task<Player_GetShowTimeRemaining_Response> Player_GetShowTimeRemaining(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetShowTimeRemaining();
                return new Player_GetShowTimeRemaining_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_GetCurrentIndex_Response> NowPlayingList_GetCurrentIndex(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_GetCurrentIndex();
                return new NowPlayingList_GetCurrentIndex_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_GetListFileUrl_Response> NowPlayingList_GetListFileUrl(NowPlayingList_GetListFileUrl_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_GetListFileUrl(request.Index);
                return new NowPlayingList_GetListFileUrl_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_GetFileProperty_Response> NowPlayingList_GetFileProperty(NowPlayingList_GetFileProperty_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_GetFileProperty(request.Index, (FilePropertyType)request.Type);
                return new NowPlayingList_GetFileProperty_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_GetFileTag_Response> NowPlayingList_GetFileTag(NowPlayingList_GetFileTag_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_GetFileTag(request.Index, (MetaDataType)request.Field);
                return new NowPlayingList_GetFileTag_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetSpectrumData_Response> NowPlaying_GetSpectrumData(NowPlaying_GetSpectrumData_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetSpectrumData(request.FftData.ToArray());
                return new NowPlaying_GetSpectrumData_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetSoundGraph_Response> NowPlaying_GetSoundGraph(NowPlaying_GetSoundGraph_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetSoundGraph(request.GraphData.ToArray());
                return new NowPlaying_GetSoundGraph_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<MB_GetLocalisation_Response> MB_GetLocalisation(MB_GetLocalisation_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_GetLocalisation(request.Id, request.DefaultText);
                return new MB_GetLocalisation_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_IsAnyPriorTracks_Response> NowPlayingList_IsAnyPriorTracks(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_IsAnyPriorTracks();
                return new NowPlayingList_IsAnyPriorTracks_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_ShowEqualiser_Response> Player_ShowEqualiser(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_ShowEqualiser();
                return new Player_ShowEqualiser_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetAutoDjEnabled_Response> Player_GetAutoDjEnabled(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetAutoDjEnabled();
                return new Player_GetAutoDjEnabled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetStopAfterCurrentEnabled_Response> Player_GetStopAfterCurrentEnabled(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetStopAfterCurrentEnabled();
                return new Player_GetStopAfterCurrentEnabled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetCrossfade_Response> Player_GetCrossfade(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetCrossfade();
                return new Player_GetCrossfade_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_SetCrossfade_Response> Player_SetCrossfade(Player_SetCrossfade_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetCrossfade(request.Crossfade);
                return new Player_SetCrossfade_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetReplayGainMode_Response> Player_GetReplayGainMode(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetReplayGainMode();
                return new Player_GetReplayGainMode_Response()
                {
                    Result = (int)result,
                };
            });
        }
        
        public override Task<Player_SetReplayGainMode_Response> Player_SetReplayGainMode(Player_SetReplayGainMode_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetReplayGainMode((ReplayGainMode)request.Mode);
                return new Player_SetReplayGainMode_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_QueueRandomTracks_Response> Player_QueueRandomTracks(Player_QueueRandomTracks_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_QueueRandomTracks(request.Count);
                return new Player_QueueRandomTracks_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Setting_GetDataType_Response> Setting_GetDataType(Setting_GetDataType_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Setting_GetDataType((MetaDataType)request.Field);
                return new Setting_GetDataType_Response()
                {
                    Result = (int)result,
                };
            });
        }
        
        public override Task<NowPlayingList_GetNextIndex_Response> NowPlayingList_GetNextIndex(NowPlayingList_GetNextIndex_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_GetNextIndex(request.Offset);
                return new NowPlayingList_GetNextIndex_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetArtistPicture_Response> NowPlaying_GetArtistPicture(NowPlaying_GetArtistPicture_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetArtistPicture(request.FadingPercent);
                return new NowPlaying_GetArtistPicture_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetDownloadedArtwork_Response> NowPlaying_GetDownloadedArtwork(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetDownloadedArtwork();
                return new NowPlaying_GetDownloadedArtwork_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<MB_ShowNowPlayingAssistant_Response> MB_ShowNowPlayingAssistant(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_ShowNowPlayingAssistant();
                return new MB_ShowNowPlayingAssistant_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetDownloadedLyrics_Response> NowPlaying_GetDownloadedLyrics(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetDownloadedLyrics();
                return new NowPlaying_GetDownloadedLyrics_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetShowRatingTrack_Response> Player_GetShowRatingTrack(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetShowRatingTrack();
                return new Player_GetShowRatingTrack_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetShowRatingLove_Response> Player_GetShowRatingLove(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetShowRatingLove();
                return new Player_GetShowRatingLove_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Setting_GetLastFmUserId_Response> Setting_GetLastFmUserId(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Setting_GetLastFmUserId();
                return new Setting_GetLastFmUserId_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_GetName_Response> Playlist_GetName(Playlist_GetName_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_GetName(request.PlaylistUrl);
                return new Playlist_GetName_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_CreatePlaylist_Response> Playlist_CreatePlaylist(Playlist_CreatePlaylist_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_CreatePlaylist(request.FolderName, request.PlaylistName, request.Filenames.ToArray());
                return new Playlist_CreatePlaylist_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_SetFiles_Response> Playlist_SetFiles(Playlist_SetFiles_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_SetFiles(request.PlaylistUrl, request.Filenames.ToArray());
                return new Playlist_SetFiles_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_QuerySimilarArtists_Response> Library_QuerySimilarArtists(Library_QuerySimilarArtists_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_QuerySimilarArtists(request.ArtistName, request.MinimumArtistSimilarityRating);
                return new Library_QuerySimilarArtists_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_QueryLookupTable_Response> Library_QueryLookupTable(Library_QueryLookupTable_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_QueryLookupTable(request.KeyTags, request.ValueTags, request.Query);
                return new Library_QueryLookupTable_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_QueryGetLookupTableValue_Response> Library_QueryGetLookupTableValue(Library_QueryGetLookupTableValue_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_QueryGetLookupTableValue(request.Key);
                return new Library_QueryGetLookupTableValue_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_QueueFilesNext_Response> NowPlayingList_QueueFilesNext(NowPlayingList_QueueFilesNext_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_QueueFilesNext(request.SourceFileUrl.ToArray());
                return new NowPlayingList_QueueFilesNext_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_QueueFilesLast_Response> NowPlayingList_QueueFilesLast(NowPlayingList_QueueFilesLast_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_QueueFilesLast(request.SourceFileUrl.ToArray());
                return new NowPlayingList_QueueFilesLast_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Setting_GetWebProxy_Response> Setting_GetWebProxy(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Setting_GetWebProxy();
                return new Setting_GetWebProxy_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_RemoveAt_Response> NowPlayingList_RemoveAt(NowPlayingList_RemoveAt_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_RemoveAt(request.Index);
                return new NowPlayingList_RemoveAt_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_RemoveAt_Response> Playlist_RemoveAt(Playlist_RemoveAt_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_RemoveAt(request.PlaylistUrl, request.Index);
                return new Playlist_RemoveAt_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<MB_OpenFilterInTab_Response> MB_OpenFilterInTab(MB_OpenFilterInTab_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_OpenFilterInTab((MetaDataType)request.Field1, (ComparisonType)request.Comparison1, request.Value1, (MetaDataType)request.Field2, (ComparisonType)request.Comparison2, request.Value2);
                return new MB_OpenFilterInTab_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<MB_SetWindowSize_Response> MB_SetWindowSize(MB_SetWindowSize_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_SetWindowSize(request.Width, request.Height);
                return new MB_SetWindowSize_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_GetArtistPicture_Response> Library_GetArtistPicture(Library_GetArtistPicture_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetArtistPicture(request.ArtistName, request.FadingPercent, request.FadingColor);
                return new Library_GetArtistPicture_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Pending_GetFileUrl_Response> Pending_GetFileUrl(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Pending_GetFileUrl();
                return new Pending_GetFileUrl_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Pending_GetFileProperty_Response> Pending_GetFileProperty(Pending_GetFileProperty_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Pending_GetFileProperty((FilePropertyType)request.Field);
                return new Pending_GetFileProperty_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Pending_GetFileTag_Response> Pending_GetFileTag(Pending_GetFileTag_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Pending_GetFileTag((MetaDataType)request.Field);
                return new Pending_GetFileTag_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetButtonEnabled_Response> Player_GetButtonEnabled(Player_GetButtonEnabled_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetButtonEnabled((PlayButtonType)request.Button);
                return new Player_GetButtonEnabled_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlayingList_MoveFiles_Response> NowPlayingList_MoveFiles(NowPlayingList_MoveFiles_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_MoveFiles(request.FromIndices.ToArray(), request.ToIndex);
                return new NowPlayingList_MoveFiles_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_GetArtworkUrl_Response> Library_GetArtworkUrl(Library_GetArtworkUrl_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetArtworkUrl(request.SourceFileUrl, request.Index);
                return new Library_GetArtworkUrl_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_GetArtistPictureThumb_Response> Library_GetArtistPictureThumb(Library_GetArtistPictureThumb_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetArtistPictureThumb(request.ArtistName);
                return new Library_GetArtistPictureThumb_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetArtworkUrl_Response> NowPlaying_GetArtworkUrl(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetArtworkUrl();
                return new NowPlaying_GetArtworkUrl_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetDownloadedArtworkUrl_Response> NowPlaying_GetDownloadedArtworkUrl(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetDownloadedArtworkUrl();
                return new NowPlaying_GetDownloadedArtworkUrl_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetArtistPictureThumb_Response> NowPlaying_GetArtistPictureThumb(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetArtistPictureThumb();
                return new NowPlaying_GetArtistPictureThumb_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_IsInList_Response> Playlist_IsInList(Playlist_IsInList_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_IsInList(request.PlaylistUrl, request.Filename);
                return new Playlist_IsInList_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_GetArtistPictureUrls_Response> Library_GetArtistPictureUrls(Library_GetArtistPictureUrls_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetArtistPictureUrls(request.ArtistName, request.LocalOnly, out var urls);
                return new Library_GetArtistPictureUrls_Response()
                {
                    Result = result,
                    Urls = { urls },
                };
            });
        }
        
        public override Task<NowPlaying_GetArtistPictureUrls_Response> NowPlaying_GetArtistPictureUrls(NowPlaying_GetArtistPictureUrls_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetArtistPictureUrls(request.LocalOnly, out var urls);
                return new NowPlaying_GetArtistPictureUrls_Response()
                {
                    Result = result,
                    Urls = { urls },
                };
            });
        }
        
        public override Task<Playlist_AppendFiles_Response> Playlist_AppendFiles(Playlist_AppendFiles_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_AppendFiles(request.PlaylistUrl, request.Filenames.ToArray());
                return new Playlist_AppendFiles_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Sync_FileStart_Response> Sync_FileStart(Sync_FileStart_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Sync_FileStart(request.Filename);
                return new Sync_FileStart_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Empty> Sync_FileEnd(Sync_FileEnd_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                _mbApi.Sync_FileEnd(request.Filename, request.Success, request.ErrorMessage);
                return new Empty();
            });
        }
        
        public override Task<Library_QueryFilesEx_Response> Library_QueryFilesEx(Library_QueryFilesEx_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_QueryFilesEx(request.Query, out var files);
                return new Library_QueryFilesEx_Response()
                {
                    Result = result,
                    Files = { files ?? Array.Empty<string>() },
                };
            });
        }
        
        public override Task<NowPlayingList_QueryFilesEx_Response> NowPlayingList_QueryFilesEx(NowPlayingList_QueryFilesEx_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_QueryFilesEx(request.Query, out var files);
                return new NowPlayingList_QueryFilesEx_Response()
                {
                    Result = result,
                    Files = { files ?? Array.Empty<string>() },
                };
            });
        }
        
        public override Task<Playlist_QueryFilesEx_Response> Playlist_QueryFilesEx(Playlist_QueryFilesEx_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_QueryFilesEx(request.PlaylistUrl, out var filenames);
                return new Playlist_QueryFilesEx_Response()
                {
                    Result = result,
                    Filenames = { filenames },
                };
            });
        }
        
        public override Task<Playlist_MoveFiles_Response> Playlist_MoveFiles(Playlist_MoveFiles_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_MoveFiles(request.PlaylistUrl, request.FromIndices.ToArray(), request.ToIndex);
                return new Playlist_MoveFiles_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_PlayNow_Response> Playlist_PlayNow(Playlist_PlayNow_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_PlayNow(request.PlaylistUrl);
                return new Playlist_PlayNow_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_IsSoundtrack_Response> NowPlaying_IsSoundtrack(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_IsSoundtrack();
                return new NowPlaying_IsSoundtrack_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<NowPlaying_GetSoundtrackPictureUrls_Response> NowPlaying_GetSoundtrackPictureUrls(NowPlaying_GetSoundtrackPictureUrls_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetSoundtrackPictureUrls(request.LocalOnly, out var urls);
                return new NowPlaying_GetSoundtrackPictureUrls_Response()
                {
                    Result = result,
                    Urls = { urls },
                };
            });
        }
        
        public override Task<Library_GetDevicePersistentId_Response> Library_GetDevicePersistentId(Library_GetDevicePersistentId_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetDevicePersistentId(request.SourceFileUrl, (DeviceIdType)request.IdType);
                return new Library_GetDevicePersistentId_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_SetDevicePersistentId_Response> Library_SetDevicePersistentId(Library_SetDevicePersistentId_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_SetDevicePersistentId(request.SourceFileUrl, (DeviceIdType)request.IdType, request.Value);
                return new Library_SetDevicePersistentId_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_FindDevicePersistentId_Response> Library_FindDevicePersistentId(Library_FindDevicePersistentId_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_FindDevicePersistentId((DeviceIdType)request.IdType, request.Ids.ToArray(), out var values);
                return new Library_FindDevicePersistentId_Response()
                {
                    Result = result,
                    Values = { values },
                };
            });
        }
        
        public override Task<Library_AddFileToLibrary_Response> Library_AddFileToLibrary(Library_AddFileToLibrary_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_AddFileToLibrary(request.SourceFileUrl, (LibraryCategory)request.Category);
                return new Library_AddFileToLibrary_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Playlist_DeletePlaylist_Response> Playlist_DeletePlaylist(Playlist_DeletePlaylist_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Playlist_DeletePlaylist(request.PlaylistUrl);
                return new Playlist_DeletePlaylist_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_GetFileTags_Response> Library_GetFileTags(Library_GetFileTags_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetFileTags(request.SourceFileUrl, request.Fields.Cast<MetaDataType>().ToArray(), out var results);
                return new Library_GetFileTags_Response()
                {
                    Result = result,
                    Results = { results },
                };
            });
        }
        
        public override Task<NowPlaying_GetFileTags_Response> NowPlaying_GetFileTags(NowPlaying_GetFileTags_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetFileTags(request.Fields.Cast<MetaDataType>().ToArray(), out var results);
                return new NowPlaying_GetFileTags_Response()
                {
                    Result = result,
                    Results = { results },
                };
            });
        }
        
        public override Task<NowPlayingList_GetFileTags_Response> NowPlayingList_GetFileTags(NowPlayingList_GetFileTags_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlayingList_GetFileTags(request.Index, request.Fields.Cast<MetaDataType>().ToArray(), out var results);
                return new NowPlayingList_GetFileTags_Response()
                {
                    Result = result,
                    Results = { results },
                };
            });
        }
        
        public override Task<MB_DownloadFile_Response> MB_DownloadFile(MB_DownloadFile_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_DownloadFile(request.Url, (DownloadTarget)request.Target, request.TargetFolder, request.CancelDownload);
                return new MB_DownloadFile_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Setting_GetFileConvertCommandLine_Response> Setting_GetFileConvertCommandLine(Setting_GetFileConvertCommandLine_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Setting_GetFileConvertCommandLine((FileCodec)request.Codec, (EncodeQuality)request.EncodeQuality);
                return new Setting_GetFileConvertCommandLine_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_OpenStreamHandle_Response> Player_OpenStreamHandle(Player_OpenStreamHandle_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_OpenStreamHandle(request.Url, request.UseMusicBeeSettings, request.EnableDsp, (ReplayGainMode)request.GainType);
                return new Player_OpenStreamHandle_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_UpdatePlayStatistics_Response> Player_UpdatePlayStatistics(Player_UpdatePlayStatistics_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_UpdatePlayStatistics(request.Url, (PlayStatisticType)request.CountType, request.DisableScrobble);
                return new Player_UpdatePlayStatistics_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Library_GetArtworkEx_Response> Library_GetArtworkEx(Library_GetArtworkEx_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_GetArtworkEx(request.SourceFileUrl, request.Index, request.RetrievePictureData, out var pictureLocations, out var pictureUrl, out var imageData);
                return new Library_GetArtworkEx_Response()
                {
                    Result = result,
                    PictureLocations = (int)pictureLocations,
                    PictureUrl = pictureUrl,
                    ImageData = ByteString.CopyFrom(imageData),
                };
            });
        }
        
        public override Task<Library_SetArtworkEx_Response> Library_SetArtworkEx(Library_SetArtworkEx_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Library_SetArtworkEx(request.SourceFileUrl, request.Index, request.ImageData.ToByteArray());
                return new Library_SetArtworkEx_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<MB_GetVisualiserInformation_Response> MB_GetVisualiserInformation(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_GetVisualiserInformation(out var visualiserNames, out var defaultVisualiserName, out var defaultState, out var currentState);
                return new MB_GetVisualiserInformation_Response()
                {
                    Result = result,
                    VisualiserNames = { visualiserNames },
                    DefaultVisualiserName = defaultVisualiserName,
                    DefaultState = (int)defaultState,
                    CurrentState = (int)currentState,
                };
            });
        }
        
        public override Task<MB_ShowVisualiser_Response> MB_ShowVisualiser(MB_ShowVisualiser_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_ShowVisualiser(request.VisualiserName, (WindowState)request.State);
                return new MB_ShowVisualiser_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<MB_GetPluginViewInformation_Response> MB_GetPluginViewInformation(MB_GetPluginViewInformation_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_GetPluginViewInformation(request.PluginFilename, out var viewNames, out var defaultViewName, out var defaultState, out var currentState);
                return new MB_GetPluginViewInformation_Response()
                {
                    Result = result,
                    ViewNames = { viewNames },
                    DefaultViewName = defaultViewName,
                    DefaultState = (int)defaultState,
                    CurrentState = (int)currentState,
                };
            });
        }
        
        public override Task<MB_ShowPluginView_Response> MB_ShowPluginView(MB_ShowPluginView_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_ShowPluginView(request.PluginFilename, request.ViewName, (WindowState)request.State);
                return new MB_ShowPluginView_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_GetOutputDevices_Response> Player_GetOutputDevices(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_GetOutputDevices(out var deviceNames, out var activeDeviceName);
                return new Player_GetOutputDevices_Response()
                {
                    Result = result,
                    DeviceNames = { deviceNames },
                    ActiveDeviceName = activeDeviceName,
                };
            });
        }
        
        public override Task<Player_SetOutputDevice_Response> Player_SetOutputDevice(Player_SetOutputDevice_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_SetOutputDevice(request.DeviceName);
                return new Player_SetOutputDevice_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<MB_UninstallPlugin_Response> MB_UninstallPlugin(MB_UninstallPlugin_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.MB_UninstallPlugin(request.PluginFilename, request.Password);
                return new MB_UninstallPlugin_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_PlayPreviousAlbum_Response> Player_PlayPreviousAlbum(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_PlayPreviousAlbum();
                return new Player_PlayPreviousAlbum_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Player_PlayNextAlbum_Response> Player_PlayNextAlbum(Empty request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Player_PlayNextAlbum();
                return new Player_PlayNextAlbum_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Podcasts_QuerySubscriptions_Response> Podcasts_QuerySubscriptions(Podcasts_QuerySubscriptions_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Podcasts_QuerySubscriptions(request.Query, out var ids);
                return new Podcasts_QuerySubscriptions_Response()
                {
                    Result = result,
                    Ids = { ids },
                };
            });
        }
        
        public override Task<Podcasts_GetSubscription_Response> Podcasts_GetSubscription(Podcasts_GetSubscription_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Podcasts_GetSubscription(request.Id, out var subscription);
                return new Podcasts_GetSubscription_Response()
                {
                    Result = result,
                    Subscription = { subscription },
                };
            });
        }
        
        public override Task<Podcasts_GetSubscriptionArtwork_Response> Podcasts_GetSubscriptionArtwork(Podcasts_GetSubscriptionArtwork_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Podcasts_GetSubscriptionArtwork(request.Id, request.Index, out var imageData);
                return new Podcasts_GetSubscriptionArtwork_Response()
                {
                    Result = result,
                    ImageData = ByteString.CopyFrom(imageData),
                };
            });
        }
        
        public override Task<Podcasts_GetSubscriptionEpisodes_Response> Podcasts_GetSubscriptionEpisodes(Podcasts_GetSubscriptionEpisodes_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Podcasts_GetSubscriptionEpisodes(request.Id, out var urls);
                return new Podcasts_GetSubscriptionEpisodes_Response()
                {
                    Result = result,
                    Urls = { urls },
                };
            });
        }
        
        public override Task<Podcasts_GetSubscriptionEpisode_Response> Podcasts_GetSubscriptionEpisode(Podcasts_GetSubscriptionEpisode_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Podcasts_GetSubscriptionEpisode(request.Id, request.Index, out var episode);
                return new Podcasts_GetSubscriptionEpisode_Response()
                {
                    Result = result,
                    Episode = { episode },
                };
            });
        }
        
        public override Task<NowPlaying_GetSoundGraphEx_Response> NowPlaying_GetSoundGraphEx(NowPlaying_GetSoundGraphEx_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.NowPlaying_GetSoundGraphEx(request.GraphData.ToArray(), request.PeakData.ToArray());
                return new NowPlaying_GetSoundGraphEx_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Sync_FileDeleteStart_Response> Sync_FileDeleteStart(Sync_FileDeleteStart_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = _mbApi.Sync_FileDeleteStart(request.Filename);
                return new Sync_FileDeleteStart_Response()
                {
                    Result = result,
                };
            });
        }
        
        public override Task<Empty> Sync_FileDeleteEnd(Sync_FileDeleteEnd_Request request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                _mbApi.Sync_FileDeleteEnd(request.Filename, request.Success, request.ErrorMessage);
                return new Empty();
            });
        }
        
    }
}
