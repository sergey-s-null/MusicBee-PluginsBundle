using Module.MusicBee.MetaInfo.Entities;
using Module.MusicBee.MetaInfo.Extensions;

namespace Module.MusicBee.MetaInfo.Proto.Services;

public sealed class ProtoNamingService
{
    private const string EmptyMessageType = "google.protobuf.Empty";

    private readonly string _requestPostfix;
    private readonly string _responsePostfix;

    public ProtoNamingService(string requestPostfix, string responsePostfix)
    {
        _requestPostfix = requestPostfix;
        _responsePostfix = responsePostfix;
    }

    public string GetRequestMessageType(MethodDefinition method)
    {
        return method.HasInputParameters()
            ? $"{method.Name}{_requestPostfix}"
            : EmptyMessageType;
    }

    public string GetResponseMessageType(MethodDefinition method)
    {
        return method.HasAnyOutputParameters()
            ? $"{method.Name}{_responsePostfix}"
            : EmptyMessageType;
    }
}