using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class StringFieldExtensions
{
    /// <summary>
    /// Начинается с
    /// </summary>
    public static BaseCondition StartsWith(this IStringField field, string value)
    {
        return ConditionsContainer.ConditionWithSingleStringFactory(field, Comparison.StartsWith, value);
    }

    /// <summary>
    /// Заканчивается
    /// </summary>
    public static BaseCondition EndsWith(this IStringField field, string value)
    {
        return ConditionsContainer.ConditionWithSingleStringFactory(field, Comparison.EndsWith, value);
    }
}