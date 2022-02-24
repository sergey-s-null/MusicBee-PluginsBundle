using System.Collections.Generic;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders.Abstract
{
    public interface IClientWrapperBuilder
    {
        string ReturnVariableName { get; set; }

        IEnumerable<string> GenerateClientWrapperLines(IReadOnlyCollection<MBApiMethodDefinition> methods);
    }
}