using System;
using System.Collections.Generic;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class StringFieldExtensions
{
    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this IStringField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this IStringField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Любое из
    /// </summary>
    public static BaseCondition IsIn(this IStringField field, params string[] values)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Любое из
    /// </summary>
    public static BaseCondition IsIn(this IStringField field, IEnumerable<string> values)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Ни одно из
    /// </summary>
    public static BaseCondition IsNotIn(this IStringField field, params string[] values)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Ни одно из
    /// </summary>
    public static BaseCondition IsNotIn(this IStringField field, IEnumerable<string> values)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не пустое значение
    /// </summary>
    public static BaseCondition IsNotNull(this IStringField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNotNull);
    }

    /// <summary>
    /// Пустое значение
    /// </summary>
    public static BaseCondition IsNull(this IStringField field)
    {
        return new ConditionWithoutValue(field, Comparison.IsNull);
    }

    /// <summary>
    /// Начинается с
    /// </summary>
    public static BaseCondition StartsWith(this IStringField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Заканчивается
    /// </summary>
    public static BaseCondition EndsWith(this IStringField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Содержит
    /// </summary>
    public static BaseCondition Contains(this IStringField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не содержит
    /// </summary>
    public static BaseCondition DoesNotContain(this IStringField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// В иерархии тегов
    /// </summary>
    public static BaseCondition InTagHierarchy(this IStringField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удовлетворяет RegEx
    /// </summary>
    public static BaseCondition MatchesRegEx(this IStringField field, string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удовлетворяет RegEx /i
    /// </summary>
    public static BaseCondition MatchesRegExIgnoreCase(this IStringField field, string value)
    {
        throw new NotImplementedException();
    }
}