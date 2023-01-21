// ReSharper disable InconsistentNaming

namespace Module.MusicBee
{
    public enum MusicBeeVersion
    {
        v2_0 = 0,
        v2_1 = 1,
        v2_2 = 2,
        v2_3 = 3,
        v2_4 = 4,
        v2_5 = 5,
        v3_0 = 6,
        v3_1 = 7
    }

    public enum PluginType
    {
        Unknown = 0,
        General = 1,
        LyricsRetrieval = 2,
        ArtworkRetrieval = 3,
        PanelView = 4,
        DataStream = 5,
        InstantMessenger = 6,
        Storage = 7,
        VideoPlayer = 8,
        DSP = 9,
        TagRetrieval = 10,
        TagOrArtworkRetrieval = 11,
        Upnp = 12,
        WebBrowser = 13
    }
}