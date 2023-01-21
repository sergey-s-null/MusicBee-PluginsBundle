using System;
using System.Collections.Generic;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class EnumFieldExtensions
{
    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this IEnumField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this IEnumField field, string value)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не содержит
    /// </summary>
    public static BaseCondition DoesNotContain(this IEnumField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// В иерархии тегов
    /// </summary>
    public static BaseCondition InTagHierarchy(this IEnumField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удовлетворяет RegEx
    /// </summary>
    public static BaseCondition MatchesRegEx(this IEnumField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удовлетворяет RegEx /i
    /// </summary>
    public static BaseCondition MatchesRegExIgnoreCase(this IEnumField field, string value)
    {
        throw new NotImplementedException();
    }
}