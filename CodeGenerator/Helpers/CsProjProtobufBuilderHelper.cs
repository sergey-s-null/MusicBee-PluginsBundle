using CodeGenerator.Builders;

namespace CodeGenerator.Helpers
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