using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class NumberFieldExtensions
{
    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this INumberField field, int value)
    {
        return ConditionsContainer.ConditionWithSingleNumberFactory(field, Comparison.Is, value);
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this INumberField field, int value)
    {
        return ConditionsContainer.ConditionWithSingleNumberFactory(field, Comparison.IsNot, value);
    }

    /// <summary>
    /// Больше, чем
    /// </summary>
    public static BaseCondition GreaterThan(this INumberField field, int value)
    {
        return ConditionsContainer.ConditionWithSingleNumberFactory(field, Comparison.GreaterThan, value);
    }

    /// <summary>
    /// Меньше, чем
    /// </summary>
    public static BaseCondition LessThan(this INumberField field, int value)
    {
        return ConditionsContainer.ConditionWithSingleNumberFactory(field, Comparison.LessThan, value);
    }

    /// <summary>
    /// В диапазоне
    /// </summary>
    public static BaseCondition InRange(this INumberField field, int from, int to)
    {
        return ConditionsContainer.ConditionWithTwoNumbersFactory(field, Comparison.InRange, from, to);
    }

    /// <summary>
    /// Не в диапазоне
    /// </summary>
    public static BaseCondition NotInRange(this INumberField field, int from, int to)
    {
        return ConditionsContainer.ConditionWithTwoNumbersFactory(field, Comparison.NotInRange, from, to);
    }

    /// <summary>
    /// Не пустое значение
    /// </summary>
    public static BaseCondition IsNotNull(this INumberField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNotNull);
    }

    /// <summary>
    /// Пустое значение
    /// </summary>
    public static BaseCondition IsNull(this INumberField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNull);
    }
}