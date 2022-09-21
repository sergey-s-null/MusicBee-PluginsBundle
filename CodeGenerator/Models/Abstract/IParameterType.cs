using System;

namespace CodeGenerator.Models.Abstract
{
    public interface IParameterType
    {
        Type Type { get; }
        bool IsNullable { get; }
    }
}