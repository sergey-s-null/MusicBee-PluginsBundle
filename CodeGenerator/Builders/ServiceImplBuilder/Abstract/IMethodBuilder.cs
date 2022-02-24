using System.Collections.Generic;
using CodeGenerator.Models;

namespace CodeGenerator.Builders.ServiceImplBuilder.Abstract
{
    public interface IMethodBuilder
    {
        IEnumerable<string> GenerateMethodLines(MBApiMethodDefinition method);
    }
}