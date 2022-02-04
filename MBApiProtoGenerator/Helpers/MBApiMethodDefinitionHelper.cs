using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Helpers
{
    public static class MBApiMethodDefinitionHelper
    {
        public static bool HasAnyParameters(this MBApiMethodDefinition methodDefinition)
        {
            return methodDefinition.HasInputParameters() 
                   || methodDefinition.HasAnyOutputParameters();
        }
        
        public static bool HasInputParameters(this MBApiMethodDefinition methodDefinition)
        {
            return methodDefinition.InputParameters.Count > 0;
        }
        
        public static bool HasAnyOutputParameters(this MBApiMethodDefinition methodDefinition)
        {
            return methodDefinition.ReturnType != typeof(void) 
                   || methodDefinition.OutputParameters.Count > 0;
        }
        
        public static bool HasReturnType(this MBApiMethodDefinition methodDefinition)
        {
            return methodDefinition.ReturnType != typeof(void);
        }
    }
}