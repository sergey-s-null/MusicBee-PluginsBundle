using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class FlagFieldExtensions
{
    /// <summary>
    /// Установлена
    /// </summary>
    public static BaseCondition IsSet(IFlagField field)
    {
        return ConditionsContainer.ConditionWithSingleStringFactory(field, Comparison.Is, string.Empty);
    }

    /// <summary>
    /// Не установлена
    /// </summary>
    public static BaseCondition IsNotSet(IFlagField field)
    {
        return ConditionsContainer.ConditionWithSingleStringFactory(field, Comparison.IsNot, string.Empty);
    }
}