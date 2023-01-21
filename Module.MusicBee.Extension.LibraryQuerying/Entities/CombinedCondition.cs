using System.Collections.Generic;
using System.Xml.Linq;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Enums;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal sealed class CombinedCondition : BaseCondition
{
    public CombineMethod CombineMethod { get; }

    private readonly IReadOnlyCollection<BaseCondition> _innerConditions;

    public CombinedCondition(CombineMethod combineMethod, IReadOnlyCollection<BaseCondition> innerConditions)
    {
        CombineMethod = combineMethod;
        _innerConditions = innerConditions;
    }

    public override XNode Build()
    {
        var andXElement = new XElement(
            "And",
            new XAttribute("CombineMethod", CombineMethod.XName)
        );

        foreach (var innerCondition in _innerConditions)
        {
            andXElement.Add(innerCondition.Build());
        }

        return new XElement(
            "Condition",
            new XAttribute("Field", Field.None.Name),
            new XAttribute("Comparison", Comparison.StartsWith.Name),
            new XAttribute("Value", string.Empty),
            andXElement
        );
    }
}