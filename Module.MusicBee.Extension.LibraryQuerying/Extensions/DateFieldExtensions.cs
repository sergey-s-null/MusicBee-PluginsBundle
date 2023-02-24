using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Enums;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class DateFieldExtensions
{
    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this IDateField field, DateTime dateTime)
    {
        return ConditionsContainer.ConditionWithSingleDateTimeFactory(field, Comparison.Is, dateTime);
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this IDateField field, DateTime dateTime)
    {
        return ConditionsContainer.ConditionWithSingleDateTimeFactory(field, Comparison.IsNot, dateTime);
    }

    /// <summary>
    /// После
    /// </summary>
    public static BaseCondition GreaterThan(this IDateField field, DateTime dateTime)
    {
        return ConditionsContainer.ConditionWithSingleDateTimeFactory(field, Comparison.GreaterThan, dateTime);
    }

    /// <summary>
    /// До
    /// </summary>
    public static BaseCondition LessThan(this IDateField field, DateTime dateTime)
    {
        return ConditionsContainer.ConditionWithSingleDateTimeFactory(field, Comparison.LessThan, dateTime);
    }

    /// <summary>
    /// За последние
    /// </summary>
    public static BaseCondition InTheLast(this IDateField field, int time, TimeUnit timeUnit)
    {
        return ConditionsContainer.ConditionWithSingleTimeOffsetFactory(
            field,
            Comparison.InTheLast,
            new TimeOffset(time, timeUnit)
        );
    }

    /// <summary>
    /// Не за последние
    /// </summary>
    public static BaseCondition NotInTheLast(this IDateField field, int time, TimeUnit timeUnit)
    {
        return ConditionsContainer.ConditionWithSingleTimeOffsetFactory(
            field,
            Comparison.NotInTheLast,
            new TimeOffset(time, timeUnit)
        );
    }

    /// <summary>
    /// Не пустое значение
    /// </summary>
    public static BaseCondition IsNotNull(this IDateField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNotNull);
    }

    /// <summary>
    /// Пустое значение
    /// </summary>
    public static BaseCondition IsNull(this IDateField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNotNull);
    }
}