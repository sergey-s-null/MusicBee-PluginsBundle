using CodeGenerator.Models;

namespace CodeGenerator.Helpers
{
    public static class MBApiMethodDefinitionHelper
    {
        public static bool HasAnyParameters(this MBApiMethodDefinition methodDefinition)
        {
            return methodDefinition.HasInputParameters() 
                   || methodDefinition.HasAnyOutputParameters();
        }
        
        public static bool HasReturnType(this MBApiMethodDefinition methodDefinition)
        {
            return methodDefinition.ReturnParameter.Type != typeof(void);
        }
    }
}