using System;
using System.Collections.Generic;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class EnumFieldExtensions
{
    private static readonly ConditionWithSingleValueFactory<string> ConditionWithSingleStringFactory;

    static EnumFieldExtensions()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this IEnumField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.Is, value);
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this IEnumField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.IsNot, value);
    }

    /// <summary>
    /// Любое из
    /// </summary>
    public static BaseCondition IsIn(this IEnumField field, params string[] values)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Любое из
    /// </summary>
    public static BaseCondition IsIn(this IEnumField field, IEnumerable<string> values)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Ни одно из
    /// </summary>
    public static BaseCondition IsNotIn(this IEnumField field, params string[] values)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Ни одно из
    /// </summary>
    public static BaseCondition IsNotIn(this IEnumField field, IEnumerable<string> values)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не пустое значение
    /// </summary>
    public static BaseCondition IsNotNull(this IEnumField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNotNull);
    }

    /// <summary>
    /// Пустое значение
    /// </summary>
    public static BaseCondition IsNull(this IEnumField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNull);
    }

    /// <summary>
    /// Содержит
    /// </summary>
    public static BaseCondition Contains(this IEnumField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.Contains, value);
    }

    /// <summary>
    /// Не содержит
    /// </summary>
    public static BaseCondition DoesNotContain(this IEnumField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.DoesNotContain, value);
    }

    /// <summary>
    /// В иерархии тегов
    /// </summary>
    public static BaseCondition InTagHierarchy(this IEnumField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.InTagHierarchy, value);
    }

    /// <summary>
    /// Удовлетворяет RegEx
    /// </summary>
    public static BaseCondition MatchesRegEx(this IEnumField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.MatchesRegEx, value);
    }

    /// <summary>
    /// Удовлетворяет RegEx /i
    /// </summary>
    public static BaseCondition MatchesRegExIgnoreCase(this IEnumField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.MatchesRegExIgnoreCase, value);
    }
}