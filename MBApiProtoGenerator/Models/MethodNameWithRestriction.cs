using MBApiProtoGenerator.Enums;

namespace MBApiProtoGenerator.Models
{
    public class MethodNameWithRestriction
    {
        public string MethodName { get; }
        public MethodRestriction Restriction { get; }

        public MethodNameWithRestriction(string methodName, 
            MethodRestriction restriction = MethodRestriction.None)
        {
            MethodName = methodName;
            Restriction = restriction;
        }
    }
}