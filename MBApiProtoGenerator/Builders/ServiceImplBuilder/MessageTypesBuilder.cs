using MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract;
using MBApiProtoGenerator.Helpers;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders.ServiceImplBuilder
{
    public class MessageTypesBuilder : IMessageTypesBuilder
    {
        private readonly IParameters _parameters;

        public MessageTypesBuilder(IParameters parameters)
        {
            _parameters = parameters;
        }

        public string GetRequestMessageType(MBApiMethodDefinition method)
        {
            return method.HasInputParameters()
                ? $"{method.Name}{_parameters.RequestPostfix}"
                : _parameters.EmptyMessageType;
        }

        public string GetResponseMessageType(MBApiMethodDefinition method)
        {
            return method.HasAnyOutputParameters()
                ? $"{method.Name}{_parameters.ResponsePostfix}"
                : _parameters.EmptyMessageType;
        }
    }
}