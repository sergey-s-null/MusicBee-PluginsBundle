using System.Xml.Linq;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Enums;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

internal sealed class CombinedCondition : BaseCondition
{
    public CombineMethod CombineMethod { get; }
    public IReadOnlyCollection<BaseCondition> InnerConditions { get; }

    public CombinedCondition(
        CombineMethod combineMethod,
        IReadOnlyCollection<BaseCondition> innerConditions)
    {
        CombineMethod = combineMethod;
        InnerConditions = innerConditions;
    }

    public override XNode Build()
    {
        var andXElement = new XElement(
            "And",
            new XAttribute("CombineMethod", CombineMethod.XName)
        );

        foreach (var innerCondition in InnerConditions)
        {
            andXElement.Add(innerCondition.Build());
        }

        return new XElement(
            "Condition",
            new XAttribute("Field", Field.Any.XName),
            new XAttribute("Comparison", Comparison.StartsWith.XName),
            new XAttribute("Value", string.Empty),
            andXElement
        );
    }
}