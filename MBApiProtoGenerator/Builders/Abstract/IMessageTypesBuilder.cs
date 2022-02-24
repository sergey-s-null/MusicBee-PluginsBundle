using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders.Abstract
{
    public interface IMessageTypesBuilder
    {
        public string GetRequestMessageType(MBApiMethodDefinition method);

        public string GetResponseMessageType(MBApiMethodDefinition method);
    }
}