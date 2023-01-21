using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal sealed class FieldWrapper :
    IExtendedStringField,
    INumberField,
    IDateField,
    IBoolField,
    IFlagField,
    IRatingField
{
    public string XName => _field.XName;

    private readonly IField _field;

    public FieldWrapper(IField field)
    {
        _field = field;
    }
}