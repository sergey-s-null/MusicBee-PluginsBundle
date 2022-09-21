using CodeGenerator.Models;

namespace CodeGenerator.Builders.ServiceImplBuilder.Abstract
{
    public interface ICommonLinesBuilder
    {
        string GetMethodDefinitionLine(MBApiMethodDefinition method);
        
        string GetMBApiCallLine(MBApiMethodDefinition method);

        string GetResponseAssignmentLine(MBApiReturnParameterDefinition parameter, string fieldName);
        string GetResponseAssignmentLine(MBApiParameterDefinition parameter);
    }
}