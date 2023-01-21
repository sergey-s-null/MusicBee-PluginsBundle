using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class UnknownTypeFieldExtensions
{
    public static IExtendedStringField AsExtendedString(this IUnknownTypeField unknownTypeField)
    {
        return unknownTypeField as IExtendedStringField
               ?? new FieldWrapper(unknownTypeField);
    }

    public static IStringField AsString(this IUnknownTypeField unknownTypeField)
    {
        return unknownTypeField as IStringField
               ?? new FieldWrapper(unknownTypeField);
    }

    public static IEnumField AsEnum(this IUnknownTypeField unknownTypeField)
    {
        return unknownTypeField as IEnumField
               ?? new FieldWrapper(unknownTypeField);
    }

    public static INumberField AsNumber(this IUnknownTypeField unknownTypeField)
    {
        return unknownTypeField as INumberField
               ?? new FieldWrapper(unknownTypeField);
    }

    public static IDateField AsDate(this IUnknownTypeField unknownTypeField)
    {
        return unknownTypeField as IDateField
               ?? new FieldWrapper(unknownTypeField);
    }

    public static IBoolField AsBool(this IUnknownTypeField unknownTypeField)
    {
        return unknownTypeField as IBoolField
               ?? new FieldWrapper(unknownTypeField);
    }

    public static IFlagField AsFlag(this IUnknownTypeField unknownTypeField)
    {
        return unknownTypeField as IFlagField
               ?? new FieldWrapper(unknownTypeField);
    }

    public static IRatingField AsRating(this IUnknownTypeField unknownTypeField)
    {
        return unknownTypeField as IRatingField
               ?? new FieldWrapper(unknownTypeField);
    }
}