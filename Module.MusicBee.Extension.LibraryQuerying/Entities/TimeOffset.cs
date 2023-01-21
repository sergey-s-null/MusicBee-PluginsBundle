using Module.MusicBee.Extension.LibraryQuerying.Enums;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal sealed class TimeOffset
{
    public int Time { get; }
    public TimeUnit TimeUnit { get; }

    public TimeOffset(int time, TimeUnit timeUnit)
    {
        Time = time;
        TimeUnit = timeUnit;
    }
}