namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal sealed class Comparison
{
    public static readonly Comparison Is = new("Is");
    public static readonly Comparison IsNot = new("IsNot");
    public static readonly Comparison IsIn = new("IsIn");
    public static readonly Comparison IsNotIn = new("IsNotIn");
    public static readonly Comparison IsNotNull = new("IsNotNull");
    public static readonly Comparison IsNull = new("IsNull");
    public static readonly Comparison StartsWith = new("StartsWith");
    public static readonly Comparison EndsWith = new("EndsWith");
    public static readonly Comparison Contains = new("Contains");
    public static readonly Comparison DoesNotContain = new("DoesNotContain");
    public static readonly Comparison InTagHierarchy = new("InTagHierarchy");
    public static readonly Comparison MatchesRegEx = new("MatchesRegEx");
    public static readonly Comparison MatchesRegExIgnoreCase = new("MatchesRegExIgnoreCase");

    public static readonly Comparison Similar = new("Similar");

    public static readonly Comparison GreaterThan = new("GreaterThan");
    public static readonly Comparison LessThan = new("LessThan");
    public static readonly Comparison InRange = new("InRange");
    public static readonly Comparison NotInRange = new("NotInRange");

    public static readonly Comparison InTheLast = new("InTheLast");
    public static readonly Comparison NotInTheLast = new("NotInTheLast");

    public string XName { get; }

    private Comparison(string xName)
    {
        XName = xName;
    }
}