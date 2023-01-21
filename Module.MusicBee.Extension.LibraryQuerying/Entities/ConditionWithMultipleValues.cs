using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal class ConditionWithMultipleValues<TValue> : BaseCondition
{
    private readonly IField _field;
    private readonly Comparison _comparison;
    private readonly IReadOnlyCollection<TValue> _values;
    private readonly Func<TValue, string> _representValue;

    public ConditionWithMultipleValues(
        IField field,
        Comparison comparison,
        IReadOnlyCollection<TValue> values,
        Func<TValue, string> representValue)
    {
        _field = field;
        _comparison = comparison;
        _values = values;
        _representValue = representValue;
    }

    public override XNode Build()
    {
        var xElement = new XElement(
            "Condition",
            new XAttribute("Field", _field.Name),
            new XAttribute("Comparison", _comparison.Name)
        );

        var i = 1;
        foreach (var value in _values)
        {
            xElement.SetAttributeValue($"Value{i}", _representValue(value));
            i++;
        }

        return xElement;
    }
}