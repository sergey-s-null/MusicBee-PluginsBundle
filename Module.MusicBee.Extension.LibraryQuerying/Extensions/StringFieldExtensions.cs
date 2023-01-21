using System;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class StringFieldExtensions
{
    private static readonly ConditionWithSingleValueFactory<string> ConditionWithSingleStringFactory;

    static StringFieldExtensions()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Начинается с
    /// </summary>
    public static BaseCondition StartsWith(this IStringField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.StartsWith, value);
    }

    /// <summary>
    /// Заканчивается
    /// </summary>
    public static BaseCondition EndsWith(this IStringField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.EndsWith, value);
    }
}