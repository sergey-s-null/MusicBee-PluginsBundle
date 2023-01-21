namespace Module.MusicBee.Extension.LibraryQuerying.Enums;

internal sealed class CombineMethod
{
    public static readonly CombineMethod All = new("All");
    public static readonly CombineMethod Any = new("Any");

    public string XName { get; }

    private CombineMethod(string xName)
    {
        XName = xName;
    }
}