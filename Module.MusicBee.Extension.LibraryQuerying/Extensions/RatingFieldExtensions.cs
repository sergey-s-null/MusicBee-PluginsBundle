using System;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Enums;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class RatingFieldExtensions
{
    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this IRatingField field, Rating rating)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this IRatingField field, Rating rating)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Больше, чем
    /// </summary>
    public static BaseCondition GreaterThan(this IRatingField field, Rating rating)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Меньше, чем
    /// </summary>
    public static BaseCondition LessThan(this IRatingField field, Rating rating)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// В диапазоне
    /// </summary>
    public static BaseCondition InRange(this IRatingField field, Rating from, Rating to)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не в диапазоне
    /// </summary>
    public static BaseCondition NotInRange(this IRatingField field, Rating from, Rating to)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не пустое значение
    /// </summary>
    public static BaseCondition IsNotNull(this IRatingField field)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Пустое значение
    /// </summary>
    public static BaseCondition IsNull(this IRatingField field)
    {
        throw new NotImplementedException();
    }
}