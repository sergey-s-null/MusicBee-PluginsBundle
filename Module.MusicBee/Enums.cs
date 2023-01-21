using System;

namespace Module.MusicBee
{
    [Flags()]
    public enum ReceiveNotificationFlags
    {
        StartupOnly = 0x0,
        PlayerEvents = 0x1,
        DataStreamEvents = 0x2,
        TagEvents = 0x04,
        DownloadEvents = 0x08
    }

    public enum NotificationType
    {
        PluginStartup = 0,          // notification sent after successful initialisation for an enabled plugin
        TrackChanging = 16,
        TrackChanged = 1,
        PlayStateChanged = 2,
        AutoDjStarted = 3,
        AutoDjStopped = 4,
        VolumeMuteChanged = 5,
        VolumeLevelChanged = 6,
        [Obsolete("Use PlayingTracksChanged")] NowPlayingListChanged = 7,
        NowPlayingListEnded = 18,
        NowPlayingArtworkReady = 8,
        NowPlayingLyricsReady = 9,
        TagsChanging = 10,
        TagsChanged = 11,
        RatingChanging = 15,
        RatingChanged = 12,
        PlayCountersChanged = 13,
        ScreenSaverActivating = 14,
        ShutdownStarted = 17,
        EmbedInPanel = 19,
        PlayerRepeatChanged = 20,
        PlayerShuffleChanged = 21,
        PlayerEqualiserOnOffChanged = 22,
        PlayerScrobbleChanged = 23,
        ReplayGainChanged = 24,
        FileDeleting = 25,
        FileDeleted = 26,
        ApplicationWindowChanged = 27,
        StopAfterCurrentChanged = 28,
        LibrarySwitched = 29,
        FileAddedToLibrary = 30,
        FileAddedToInbox = 31,
        SynchCompleted = 32,
        DownloadCompleted = 33,
        MusicBeeStarted = 34,
        PlayingTracksChanged = 35,
        PlayingTracksQueueChanged = 36,
		PlaylistCreated = 37,
		PlaylistUpdated = 38,
		PlaylistDeleted = 39
    }

    public enum PluginCloseReason
    {
        MusicBeeClosing = 1,
        UserDisabled = 2,
        StopNoUnload = 3
    }

    public enum CallbackType
    {
        SettingsUpdated = 1,
        StorageReady = 2,
        StorageFailed = 3,
        FilesRetrievedChanged = 4,
        FilesRetrievedNoChange = 5,
        FilesRetrievedFail = 6,
        LyricsDownloaded = 7,
        StorageEject = 8,
        SuspendPlayCounters = 9,
        ResumePlayCounters = 10,
        EnablePlugin = 11,
        DisablePlugin = 12,
        RenderingDevicesChanged = 13,
        FullscreenOn = 14,
        FullscreenOff = 15
    }

    public enum FilePropertyType
    {
        Url = 2,
        Kind = 4,
        Format = 5,
        Size = 7,
        Channels = 8,
        SampleRate = 9,
        Bitrate = 10,
        DateModified = 11,
        DateAdded = 12,
        LastPlayed = 13,
        PlayCount = 14,
        SkipCount = 15,
        Duration = 16,
        Status = 21,
        NowPlayingListIndex = 78,  // only has meaning when called from NowPlayingList_* commands
        ReplayGainTrack = 94,
        ReplayGainAlbum = 95
    }

    public enum MetaDataType
    {
        TrackTitle = 65,
        Album = 30,
        AlbumArtist = 31,        // displayed album artist
        AlbumArtistRaw = 34,     // stored album artist
        Artist = 32,             // displayed artist
        MultiArtist = 33,        // individual artists, separated by a null char
        /// <summary>
        /// Первый артист из списка артистов, иначе отображаемый артист
        /// </summary>
		PrimaryArtist = 19,
        Artists = 144,
        ArtistsWithArtistRole = 145,
        ArtistsWithPerformerRole = 146,
        ArtistsWithGuestRole = 147,
        ArtistsWithRemixerRole = 148,
        Artwork = 40,
        BeatsPerMin = 41,
        Composer = 43,           // displayed composer
        MultiComposer = 89,      // individual composers, separated by a null char
        /// <summary>
        /// Комментарий
        /// </summary>
        Comment = 44,
        Conductor = 45,
        Custom1 = 46,
        Custom2 = 47,
        Custom3 = 48,
        Custom4 = 49,
        Custom5 = 50,
        Custom6 = 96,
        Custom7 = 97,
        Custom8 = 98,
        Custom9 = 99,
        Custom10 = 128,
        Custom11 = 129,
        Custom12 = 130,
        Custom13 = 131,
        Custom14 = 132,
        Custom15 = 133,
        Custom16 = 134,
        DiscNo = 52,
        DiscCount = 54,
        Encoder = 55,
        Genre = 59,
        Genres = 143,
        GenreCategory = 60,
        Grouping = 61,
        Keywords = 84,
        HasLyrics = 63,
        Lyricist = 62,
        Lyrics = 114,
        Mood = 64,
        Occasion = 66,
        Origin = 67,
        Publisher = 73,
        Quality = 74,
        Rating = 75,
        RatingLove = 76,
        RatingAlbum = 104,
        Tempo = 85,
        TrackNo = 86,
        TrackCount = 87,
        Virtual1 = 109,
        Virtual2 = 110,
        Virtual3 = 111,
        Virtual4 = 112,
        Virtual5 = 113,
        Virtual6 = 122,
        Virtual7 = 123,
        Virtual8 = 124,
        Virtual9 = 125,
        Virtual10 = 135,
        Virtual11 = 136,
        Virtual12 = 137,
        Virtual13 = 138,
        Virtual14 = 139,
        Virtual15 = 140,
        Virtual16 = 141,
        Virtual17 = 149,
        Virtual18 = 150,
        Virtual19 = 151,
        Virtual20 = 152,
        Virtual21 = 153,
        Virtual22 = 154,
        Virtual23 = 155,
        Virtual24 = 156,
        Virtual25 = 157,
        Year = 88,
        /// <summary>
        /// Вероятно, по умолчанию может отличаться. Например "The" переносит в конец.
        /// </summary>
        SortTitle = 163,
        SortAlbum = 164,
        SortAlbumArtist = 165,
        SortArtist = 166,
        SortComposer = 167,
        Work = 168,
        MovementName = 169,
        MovementNo = 170,
        MovementCount = 171,
        ShowMovement = 172,
        Language = 173,
        /// <summary>
        /// Первоначальный исполнитель
        /// </summary>
        OriginalArtist = 174,
        /// <summary>
        /// Год первоначального исполнения
        /// </summary>
        OriginalYear = 175,
        /// <summary>
        /// Первоначальное название
        /// </summary>
        OriginalTitle = 177
    }

    public enum FileCodec
    {
        Unknown = -1,
        Mp3 = 1,
        Aac = 2,
        Flac = 3,
        Ogg = 4,
        WavPack = 5,
        Wma = 6,
        Tak = 7,
        Mpc = 8,
        Wave = 9,
        Asx = 10,
        Alac = 11,
        Aiff = 12,
        Pcm = 13,
        Opus = 15,
        Spx = 16,
        Dsd = 17,
        AacNoContainer = 18
    }

    public enum EncodeQuality
    {
        SmallSize = 1,
        Portable = 2,
        HighQuality = 3,
        Archiving = 4
    }

    [Flags()]
    public enum LibraryCategory
    {
        Music = 0,
        Audiobook = 1,
        Video = 2,
        Inbox = 4
    }

    public enum DeviceIdType
    {
        MusicBeeNativeId = 0,
        GooglePlay = 1,
        AppleDevice = 2,
        GooglePlay2 = 3,
        AppleDevice2 = 4,
		WebDrivePluginOneDrive = 5,
		WebDrivePluginGoogleDrive = 6,
		WebDrivePluginDropBox = 7
    }

    public enum DataType
    {
        String = 0,
        Number = 1,
        DateTime = 2,
        Rating = 3
    }

    public enum SettingId
    {
        CompactPlayerFlickrEnabled = 1,
        FileTaggingPreserveModificationTime = 2,
        LastDownloadFolder = 3,
        ArtistGenresOnly = 4,
        IgnoreNamePrefixes = 5,
        IgnoreNameChars = 6,
        PlayCountTriggerPercent = 7,
        PlayCountTriggerSeconds = 8,
        SkipCountTriggerPercent = 9,
        SkipCountTriggerSeconds = 10,
        CustomWebLinkName1 = 11,
        CustomWebLinkName2 = 12,
        CustomWebLinkName3 = 13,
        CustomWebLinkName4 = 14,
        CustomWebLinkName5 = 15,
        CustomWebLinkName6 = 16,
        CustomWebLinkName7 = 29,
        CustomWebLinkName8 = 30,
        CustomWebLinkName9 = 31,
        CustomWebLinkName10 = 32,
        CustomWebLink1 = 17,
        CustomWebLink2 = 18,
        CustomWebLink3 = 19,
        CustomWebLink4 = 20,
        CustomWebLink5 = 21,
        CustomWebLink6 = 22,
        CustomWebLink7 = 33,
        CustomWebLink8 = 34,
        CustomWebLink9 = 35,
        CustomWebLink10 = 36,
        CustomWebLinkNowPlaying1 = 23,
        CustomWebLinkNowPlaying2 = 24,
        CustomWebLinkNowPlaying3 = 25,
        CustomWebLinkNowPlaying4 = 26,
        CustomWebLinkNowPlaying5 = 27,
        CustomWebLinkNowPlaying6 = 28,
        CustomWebLinkNowPlaying7 = 37,
        CustomWebLinkNowPlaying8 = 38,
        CustomWebLinkNowPlaying9 = 39,
        CustomWebLinkNowPlaying10 = 40
    }

    public enum ComparisonType
    {
        Is = 0,
        IsNot = 1,
        Contains = 4,
        DoesNotContain = 5,
        StartsWith = 6,
        EndsWith = 7,
        IsSimilar = 20
    }

    public enum LyricsType
    {
        NotSpecified = 0,
        Synchronised = 1,
        UnSynchronised = 2
    }

    public enum PlayState
    {
        Undefined = 0,
        Loading = 1,
        Playing = 3,
        Paused = 6,
        Stopped = 7
    }

    public enum RepeatMode
    {
        None = 0,
        All = 1,
        One = 2
    }

    public enum PlayButtonType
    {
        PreviousTrack = 0,
        PlayPause = 1,
        NextTrack = 2,
        Stop = 3
    }

    public enum PlaylistFormat
    {
        Unknown = 0,
        M3u = 1,
        Xspf = 2,
        Asx = 3,
        Wpl = 4,
        Pls = 5,
        Auto = 7,
        M3uAscii = 8,
        AsxFile = 9,
        Radio = 10,
        M3uExtended = 11,
        Mbp = 12
    }

    public enum SkinElement
    {
        SkinSubPanel = 0,
        SkinInputControl = 7,
        SkinInputPanel = 10,
        SkinInputPanelLabel = 14,
        SkinTrackAndArtistPanel = -1
    }

    public enum ElementState
    {
        ElementStateDefault = 0,
        ElementStateModified = 6
    }

    public enum ElementComponent
    {
        ComponentBorder = 0,
        ComponentBackground = 1,
        ComponentForeground = 3
    }

    public enum PluginPanelDock
    {
        ApplicationWindow = 0,
        TrackAndArtistPanel = 1,
        TextBox = 3,
        ComboBox = 4,
        MainPanel = 5
    }

    
    public enum ReplayGainMode
    {
        Off = 0,
        Track = 1,
        Album = 2,
        Smart = 3
    }
    
    public enum PlayStatisticType
    {
        NoChange = 0,
        IncreasePlayCount = 1,
        IncreaseSkipCount = 2
    }

    public enum Command
    {
        NavigateTo = 1
    }
    
    public enum DownloadTarget
    {
        Inbox = 0,
        MusicLibrary = 1,
        SpecificFolder = 3
    }

    [Flags()]
    public enum PictureLocations: byte
    {
        None = 0,
        EmbedInFile = 1,
        LinkToOrganisedCopy = 2,
        LinkToSource = 4,
        FolderThumb = 8
    }

    public enum WindowState
    {
        Off = -1,
        Normal = 0,
        Fullscreen = 1,
        Desktop = 2
    }

    public enum SubscriptionMetaDataType
    {
        Id = 0,
        Title = 1,
        Grouping = 2,
        Genre = 3,
        Description = 4,
        DounloadedCount = 5
    }

    public enum EpisodeMetaDataType
    {
        Id = 0,
        Title = 1,
        DateTime = 2,
        Description = 3,
        Duration = 4,
        IsDownloaded = 5,
        HasBeenPlayed = 6
    }
}