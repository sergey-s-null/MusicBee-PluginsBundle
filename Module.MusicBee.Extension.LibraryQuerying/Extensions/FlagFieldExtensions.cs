using System;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class FlagFieldExtensions
{
    private static readonly ConditionWithSingleValueFactory<string> ConditionWithSingleStringFactory;

    static FlagFieldExtensions()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Установлена
    /// </summary>
    public static BaseCondition IsSet(IFlagField field)
    {
        return ConditionWithSingleStringFactory(field, Comparison.Is, string.Empty);
    }

    /// <summary>
    /// Не установлена
    /// </summary>
    public static BaseCondition IsNotSet(IFlagField field)
    {
        return ConditionWithSingleStringFactory(field, Comparison.IsNot, string.Empty);
    }
}