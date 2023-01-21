namespace Module.MusicBee.Extension.LibraryQuerying.Enums;

public sealed class TimeUnit
{
    public static readonly TimeUnit Minutes = new("h");
    public static readonly TimeUnit Days = new("d");
    public static readonly TimeUnit Weeks = new("w");
    public static readonly TimeUnit Months = new("m");

    public string Postfix { get; }

    private TimeUnit(string postfix)
    {
        Postfix = postfix;
    }
}