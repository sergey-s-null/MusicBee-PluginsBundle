using System;
using System.Xml.Linq;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal sealed class ConditionWithSingleValue<TValue> : BaseCondition
{
    private readonly IField _field;
    private readonly Comparison _comparison;
    private readonly TValue _value;
    private readonly Func<TValue, string> _formatValue;

    public ConditionWithSingleValue(
        IField field,
        Comparison comparison,
        TValue value,
        Func<TValue, string> formatValue)
    {
        _field = field;
        _comparison = comparison;
        _value = value;
        _formatValue = formatValue;
    }

    public override XNode Build()
    {
        return new XElement(
            "Condition",
            new XAttribute("Field", _field.Name),
            new XAttribute("Comparison", _comparison.Name),
            new XAttribute("Value", _formatValue(_value))
        );
    }
}