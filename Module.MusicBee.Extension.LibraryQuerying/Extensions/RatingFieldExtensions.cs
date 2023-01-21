using System;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Enums;
using Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class RatingFieldExtensions
{
    private static readonly ConditionWithSingleValueFactory<Rating> ConditionWithSingleRatingFactory;
    private static readonly ConditionWithTwoValuesFactory<Rating> ConditionWithTwoRatingsFactory;

    static RatingFieldExtensions()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this IRatingField field, Rating rating)
    {
        return ConditionWithSingleRatingFactory(field, Comparison.Is, rating);
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this IRatingField field, Rating rating)
    {
        return ConditionWithSingleRatingFactory(field, Comparison.IsNot, rating);
    }

    /// <summary>
    /// Больше, чем
    /// </summary>
    public static BaseCondition GreaterThan(this IRatingField field, Rating rating)
    {
        return ConditionWithSingleRatingFactory(field, Comparison.GreaterThan, rating);
    }

    /// <summary>
    /// Меньше, чем
    /// </summary>
    public static BaseCondition LessThan(this IRatingField field, Rating rating)
    {
        return ConditionWithSingleRatingFactory(field, Comparison.LessThan, rating);
    }

    /// <summary>
    /// В диапазоне
    /// </summary>
    public static BaseCondition InRange(this IRatingField field, Rating from, Rating to)
    {
        return ConditionWithTwoRatingsFactory(field, Comparison.InRange, from, to);
    }

    /// <summary>
    /// Не в диапазоне
    /// </summary>
    public static BaseCondition NotInRange(this IRatingField field, Rating from, Rating to)
    {
        return ConditionWithTwoRatingsFactory(field, Comparison.NotInRange, from, to);
    }

    /// <summary>
    /// Не пустое значение
    /// </summary>
    public static BaseCondition IsNotNull(this IRatingField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNotNull);
    }

    /// <summary>
    /// Пустое значение
    /// </summary>
    public static BaseCondition IsNull(this IRatingField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNull);
    }
}