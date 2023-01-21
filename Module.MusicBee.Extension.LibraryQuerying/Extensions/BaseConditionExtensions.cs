using System.Collections.Generic;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Enums;

namespace Module.MusicBee.Extension.LibraryQuerying.Extensions;

public static class BaseConditionExtensions
{
    public static BaseCondition And(this BaseCondition condition, BaseCondition another)
    {
        return Combine(condition, another, CombineMethod.All);
    }

    public static BaseCondition Or(this BaseCondition condition, BaseCondition another)
    {
        return Combine(condition, another, CombineMethod.Any);
    }

    private static BaseCondition Combine(
        BaseCondition first,
        BaseCondition second,
        CombineMethod combineMethod)
    {
        return new CombinedCondition(
            combineMethod,
            Unwrap(first, second, combineMethod)
        );
    }

    private static IReadOnlyCollection<BaseCondition> Unwrap(
        BaseCondition first,
        BaseCondition second,
        CombineMethod combineMethod)
    {
        var unwrapped = new List<BaseCondition>();

        if (first is CombinedCondition firstCombined
            && firstCombined.CombineMethod == combineMethod)
        {
            unwrapped.AddRange(firstCombined.InnerConditions);
        }
        else
        {
            unwrapped.Add(first);
        }

        if (second is CombinedCondition secondCombined
            && secondCombined.CombineMethod == combineMethod)
        {
            unwrapped.AddRange(secondCombined.InnerConditions);
        }
        else
        {
            unwrapped.Add(second);
        }

        return unwrapped;
    }
}