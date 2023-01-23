using System.Linq;
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

    public static bool HasReturnType(this MethodDefinition methodDefinition)
    {
        return methodDefinition.ReturnParameter.Type != typeof(void);
    }

    public static string GetClassMethodArguments(this MethodDefinition method)
    {
        var inParams = method.InputParameters
            .Select(x => $"{x.GetCSharpTypeName()} {x.Name}");
        var outParams = method.OutputParameters
            .Select(x => $"out {x.GetCSharpTypeName()} {x.Name}");
        return string.Join(", ", inParams.Concat(outParams));
    }

    public static string GetMethodCallingArguments(this MethodDefinition method)
    {
        var inParams = method.InputParameters
            .Select(x => x.Name);
        var outParams = method.OutputParameters
            .Select(x => $"out {x.Name}");
        return string.Join(", ", inParams.Concat(outParams));
    }
}