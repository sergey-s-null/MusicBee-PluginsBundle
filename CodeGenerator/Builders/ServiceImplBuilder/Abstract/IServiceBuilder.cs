using System.Collections.Generic;
using CodeGenerator.Models;

namespace CodeGenerator.Builders.ServiceImplBuilder.Abstract
{
    public interface IServiceBuilder
    {
        IEnumerable<string> GenerateServiceLines(IReadOnlyCollection<MBApiMethodDefinition> methods);
    }
}