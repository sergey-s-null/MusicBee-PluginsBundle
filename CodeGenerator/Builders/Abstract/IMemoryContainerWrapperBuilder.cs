using System.Collections.Generic;
using CodeGenerator.Models;

namespace CodeGenerator.Builders.Abstract
{
    public interface IMemoryContainerWrapperBuilder
    {
        string Namespace { get; set; }

        IEnumerable<string> GenerateMemoryContainerWrapperLines(
            IReadOnlyCollection<MBApiMethodDefinition> methods);
    }
}