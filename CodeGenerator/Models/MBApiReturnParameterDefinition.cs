using System;
using CodeGenerator.Models.Abstract;

namespace CodeGenerator.Models
{
    public class MBApiReturnParameterDefinition : IParameterType
    {
        public Type Type { get; }
        public bool IsNullable { get; }

        public MBApiReturnParameterDefinition(Type type, bool isNullable)
        {
            Type = type;
            IsNullable = isNullable;
        }
    }
}