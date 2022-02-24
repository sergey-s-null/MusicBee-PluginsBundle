using System.Collections.Generic;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders.Abstract
{
    public interface IMemoryContainerWrapperBuilder
    {
        string Namespace { get; set; }

        IEnumerable<string> GenerateMemoryContainerWrapperLines(
            IReadOnlyCollection<MBApiMethodDefinition> methods);
    }
}