using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

public sealed class StringField : IStringField
{
    public string Name { get; }

    public StringField(string name)
    {
        Name = name;
    }
}