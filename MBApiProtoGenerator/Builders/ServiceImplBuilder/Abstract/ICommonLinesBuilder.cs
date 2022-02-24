using System;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract
{
    public interface ICommonLinesBuilder
    {
        string GetMethodDefinitionLine(MBApiMethodDefinition method);
        
        string GetMBApiCallLine(MBApiMethodDefinition method);
        
        string GetResponseAssignmentLine(MBApiParameterDefinition parameter);
        string GetResponseAssignmentLine(Type parameterType, string parameterName);
    }
}