using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class BoolFieldExtensions
{
    public static BaseCondition IsTrue(this IBoolField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNotNull);
    }

    public static BaseCondition IsFalse(this IBoolField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNull);
    }
}