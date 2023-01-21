using System;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class NumberFieldExtensions
{
    private static readonly ConditionWithSingleValueFactory<int> ConditionWithSingleValueFactory;

    static NumberFieldExtensions()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this INumberField field, int value)
    {
        return ConditionWithSingleValueFactory(field, Comparison.Is, value);
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this INumberField field, int value)
    {
        return ConditionWithSingleValueFactory(field, Comparison.IsNot, value);
    }

    /// <summary>
    /// Больше, чем
    /// </summary>
    public static BaseCondition GreaterThan(this INumberField field, int value)
    {
        return ConditionWithSingleValueFactory(field, Comparison.GreaterThan, value);
    }

    /// <summary>
    /// Меньше, чем
    /// </summary>
    public static BaseCondition LessThan(this INumberField field, int value)
    {
        return ConditionWithSingleValueFactory(field, Comparison.LessThan, value);
    }

    /// <summary>
    /// В диапазоне
    /// </summary>
    public static BaseCondition InRange(this INumberField field, int from, int to)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не в диапазоне
    /// </summary>
    public static BaseCondition NotInRange(this INumberField field, int from, int to)
    {
        throw new NotImplementedException();
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