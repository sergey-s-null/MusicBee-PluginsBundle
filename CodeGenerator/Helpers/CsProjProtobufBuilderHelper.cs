using MBApiProtoGenerator.Builders;

namespace MBApiProtoGenerator.Helpers
{
    public static class CsProjProtobufBuilderHelper
    {
        public static CsProjProtobufBuilder AddProtobufItemGroup(this CsProjProtobufBuilder builder,
            params string[] protoFilePaths)
        {
            return builder.AddProtobufItemGroup(protoFilePaths);
        }
    }
}