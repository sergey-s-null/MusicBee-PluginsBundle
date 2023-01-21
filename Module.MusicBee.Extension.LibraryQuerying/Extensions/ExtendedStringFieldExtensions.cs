using System;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class ExtendedStringFieldExtensions
{
    private static readonly ConditionWithSingleValueFactory<string> ConditionWithSingleStringFactory;

    static ExtendedStringFieldExtensions()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Похожий на
    /// </summary>
    public static BaseCondition Similar(this IExtendedStringField field, string value)
    {
        return ConditionWithSingleStringFactory(field, Comparison.Similar, value);
    }
}