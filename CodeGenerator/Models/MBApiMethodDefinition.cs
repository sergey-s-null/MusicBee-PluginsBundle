using System.Collections.Generic;

namespace CodeGenerator.Models
{
    public class MBApiMethodDefinition
    {
        public string Name { get; }
        public IReadOnlyCollection<MBApiParameterDefinition> InputParameters { get; }
        public IReadOnlyCollection<MBApiParameterDefinition> OutputParameters { get; }
        public MBApiReturnParameterDefinition ReturnParameter { get; }

        public MBApiMethodDefinition(
            string name,
            IReadOnlyCollection<MBApiParameterDefinition> inputParameters,
            IReadOnlyCollection<MBApiParameterDefinition> outputParameters,
            MBApiReturnParameterDefinition returnParameter)
        {
            Name = name;
            InputParameters = inputParameters;
            OutputParameters = outputParameters;
            ReturnParameter = returnParameter;
        }
    }
}