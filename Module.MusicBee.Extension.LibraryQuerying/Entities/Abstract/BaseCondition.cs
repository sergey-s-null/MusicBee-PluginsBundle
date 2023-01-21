using System;
using System.Xml.Linq;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

public abstract class BaseCondition
{
    public abstract XNode Build();

    public static BaseCondition operator &(BaseCondition first, BaseCondition second)
    {
        throw new NotImplementedException();
    }
}