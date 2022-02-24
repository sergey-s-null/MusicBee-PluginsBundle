using MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract;

namespace MBApiProtoGenerator.Builders.ServiceImplBuilder
{
    public class HardcodedServiceBuilderParameters : IServiceBuilderParameters
    {
        public string RequestPostfix => "_Request";
        public string ResponsePostfix => "_Response";
        public string EmptyMessageType => "Empty";
        public string MBApiVariableName => "mbApi";
        public string ReturnParameterName => "result";
        public bool WithDispatcher => false;
    }
}