using System.Xml.Linq;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal sealed class ConditionWithoutValue : BaseCondition
{
    private readonly IField _field;
    private readonly Comparison _comparison;

    public ConditionWithoutValue(IField field, Comparison comparison)
    {
        _field = field;
        _comparison = comparison;
    }

    public override XNode Build()
    {
        return new XElement(
            "Condition",
            new XAttribute("Field", _field.Name),
            new XAttribute("Comparison", _comparison.Name)
        );
    }
}