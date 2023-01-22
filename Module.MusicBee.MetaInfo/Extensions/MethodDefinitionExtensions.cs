using Module.MusicBee.MetaInfo.Entities;

namespace Module.MusicBee.MetaInfo.Extensions;

public static class MethodDefinitionExtensions
{
    public static bool HasAnyOutputParameters(this MethodDefinition methodDefinition)
    {
        return methodDefinition.ReturnParameter.Type != typeof(void)
               || methodDefinition.OutputParameters.Count > 0;
    }

    public static bool HasInputParameters(this MethodDefinition methodDefinition)
    {
        return methodDefinition.InputParameters.Count > 0;
    }
}