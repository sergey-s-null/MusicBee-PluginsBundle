using System;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class NumberFieldExtensions
{
    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this INumberField field, int value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this INumberField field, int value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Больше, чем
    /// </summary>
    public static BaseCondition GreaterThan(this INumberField field, int value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Меньше, чем
    /// </summary>
    public static BaseCondition LessThan(this INumberField field, int value)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    /// <summary>
    /// Пустое значение
    /// </summary>
    public static BaseCondition IsNull(this INumberField field)
    {
        throw new NotImplementedException();
    }
}