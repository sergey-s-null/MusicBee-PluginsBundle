using Module.Core.GUI.Converters.Enums;

namespace Module.Core.Extensions;

public static class CompareOperatorExtensions
{
    public static bool Compare(this CompareOperator compareOperator, int first, int second)
    {
        return compareOperator switch
        {
            CompareOperator.Equal => first == second,
            CompareOperator.NotEqual => first != second,
            CompareOperator.LessThan => first < second,
            CompareOperator.LessThanOrEqual => first <= second,
            CompareOperator.GreaterThan => first > second,
            CompareOperator.GreaterThanOrEqual => first >= second,
            _ => throw new ArgumentOutOfRangeException(nameof(compareOperator), compareOperator, null)
        };
    }
}