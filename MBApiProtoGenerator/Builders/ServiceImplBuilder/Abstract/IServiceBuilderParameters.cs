namespace MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract
{
    public interface IServiceBuilderParameters
    {
        string RequestPostfix { get; }
        string ResponsePostfix { get; }
        string EmptyMessageType { get; }
        string MBApiVariableName { get; }
        string ReturnParameterName { get; }
        bool WithDispatcher { get; }
    }
}