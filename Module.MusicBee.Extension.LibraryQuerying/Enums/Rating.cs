namespace Module.MusicBee.Extension.LibraryQuerying.Enums;

public sealed class Rating
{
    public static readonly Rating Unset = new(-1);

    public static readonly Rating Half = new(10);
    public static readonly Rating One = new(20);
    public static readonly Rating OneAndHalf = new(30);
    public static readonly Rating Two = new(40);
    public static readonly Rating TwoAndHalf = new(50);
    public static readonly Rating Three = new(60);
    public static readonly Rating ThreeAndHalf = new(70);
    public static readonly Rating Four = new(80);
    public static readonly Rating FourAndHalf = new(90);
    public static readonly Rating Five = new(100);

    public int Value { get; }

    private Rating(int value)
    {
        Value = value;
    }
}