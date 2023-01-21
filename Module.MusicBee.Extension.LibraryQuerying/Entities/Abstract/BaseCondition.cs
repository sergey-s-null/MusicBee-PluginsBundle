using System.Xml.Linq;
using Module.MusicBee.Extension.LibraryQuerying.Extensions;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

public abstract class BaseCondition
{
    public abstract XNode Build();

    public static BaseCondition operator &(BaseCondition first, BaseCondition second)
    {
        return first.And(second);
    }

    public static BaseCondition operator |(BaseCondition first, BaseCondition second)
    {
        return first.Or(second);
    }
}