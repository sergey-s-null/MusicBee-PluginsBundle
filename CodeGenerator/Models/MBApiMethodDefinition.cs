using System;
using System.Collections.Generic;

namespace CodeGenerator.Models
{
    public class MBApiMethodDefinition
    {
        public string Name { get; }
        public IReadOnlyCollection<MBApiParameterDefinition> InputParameters { get; }
        public IReadOnlyCollection<MBApiParameterDefinition> OutputParameters { get; }
        public Type ReturnType { get; }

        public MBApiMethodDefinition(string name, IReadOnlyCollection<MBApiParameterDefinition> inputParameters,
            IReadOnlyCollection<MBApiParameterDefinition> outputParameters, Type returnType)
        {
            Name = name;
            InputParameters = inputParameters;
            OutputParameters = outputParameters;
            ReturnType = returnType;
        }
    }
}