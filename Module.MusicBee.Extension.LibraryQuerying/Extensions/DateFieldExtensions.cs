using System;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Enums;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class DateFieldExtensions
{
    /// <summary>
    /// Равно
    /// </summary>
    public static BaseCondition Is(this IDateField field, DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не равно
    /// </summary>
    public static BaseCondition IsNot(this IDateField field, DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// После
    /// </summary>
    public static BaseCondition GreaterThan(this IDateField field, DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// До
    /// </summary>
    public static BaseCondition LessThan(this IDateField field, DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// За последние
    /// </summary>
    public static BaseCondition InTheLast(this IDateField field, int time, TimeUnit timeUnit)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не за последние
    /// </summary>
    public static BaseCondition NotInTheLast(this IDateField field, int time, TimeUnit timeUnit)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Не пустое значение
    /// </summary>
    public static BaseCondition IsNotNull(this IDateField field)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Пустое значение
    /// </summary>
    public static BaseCondition IsNull(this IDateField field)
    {
        throw new NotImplementedException();
    }
}