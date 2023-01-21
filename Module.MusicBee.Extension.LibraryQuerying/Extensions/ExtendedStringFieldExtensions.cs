using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class ExtendedStringFieldExtensions
{
    /// <summary>
    /// Похожий на
    /// </summary>
    public static BaseCondition Similar(this IExtendedStringField field, string value)
    {
        return ConditionsContainer.ConditionWithSingleStringFactory(field, Comparison.Similar, value);
    }
}