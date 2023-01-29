using Mead.MusicBee.MetaInfo.Entities;
using Mead.MusicBee.MetaInfo.Extensions;

namespace Module.MusicBee.MetaInfo.Proto.Services;

public sealed class ProtoNamingService
{
    private readonly string _emptyMessageType;
    private readonly string _requestPostfix;
    private readonly string _responsePostfix;

    public ProtoNamingService(
        string emptyMessageType,
        string requestPostfix,
        string responsePostfix)
    {
        _emptyMessageType = emptyMessageType;
        _requestPostfix = requestPostfix;
        _responsePostfix = responsePostfix;
    }

    public string GetRequestMessageType(MethodDefinition method)
    {
        return method.HasInputParameters()
            ? $"{method.Name}{_requestPostfix}"
            : _emptyMessageType;
    }

    public string GetResponseMessageType(MethodDefinition method)
    {
        return method.HasAnyOutputParameters()
            ? $"{method.Name}{_responsePostfix}"
            : _emptyMessageType;
    }
}