using System.Xml.Linq;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal sealed class ConditionWithSingleValue<TValue> : BaseCondition
{
    private readonly IField _field;
    private readonly Comparison _comparison;
    private readonly TValue _value;
    private readonly Func<TValue, string> _representValue;

    public ConditionWithSingleValue(
        IField field,
        Comparison comparison,
        TValue value,
        Func<TValue, string> representValue)
    {
        _field = field;
        _comparison = comparison;
        _value = value;
        _representValue = representValue;
    }

    public override XNode Build()
    {
        return new XElement(
            "Condition",
            new XAttribute("Field", _field.XName),
            new XAttribute("Comparison", _comparison.XName),
            new XAttribute("Value", _representValue(_value))
        );
    }
}