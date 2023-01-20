using System;
using CodeGenerator.Models.Abstract;

namespace CodeGenerator.Models
{
    public sealed class MBApiParameterDefinition : IParameterType
    {
        public Type Type { get; }
        public string Name { get; }
        public bool IsNullable { get; }

        public MBApiParameterDefinition(Type type, string name, bool isNullable)
        {
            Type = type;
            Name = name;
            IsNullable = isNullable;
        }
    }
}