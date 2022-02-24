using System;

namespace MBApiProtoGenerator.Models
{
    public class MBApiParameterDefinition
    {
        public Type Type { get; }
        public string Name { get; }

        public MBApiParameterDefinition(Type type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}