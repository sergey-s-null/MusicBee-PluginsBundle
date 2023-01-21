using System;
using System.Xml.Linq;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal class ConditionWithTwoValues<TValue> : BaseCondition
{
    private readonly IField _field;
    private readonly Comparison _comparison;
    private readonly TValue _firstValue;
    private readonly TValue _secondValue;
    private readonly Func<TValue, string> _representValue;

    public ConditionWithTwoValues(
        IField field,
        Comparison comparison,
        TValue firstValue,
        TValue secondValue,
        Func<TValue, string> representValue)
    {
        _field = field;
        _comparison = comparison;
        _firstValue = firstValue;
        _secondValue = secondValue;
        _representValue = representValue;
    }

    public override XNode Build()
    {
        return new XElement(
            "Condition",
            new XAttribute("Field", _field.Name),
            new XAttribute("Comparison", _comparison.Name),
            new XAttribute("Value1", _representValue(_firstValue)),
            new XAttribute("Value2", _representValue(_secondValue))
        );
    }
}