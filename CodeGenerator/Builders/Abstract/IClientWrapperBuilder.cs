using System.Collections.Generic;
using CodeGenerator.Models;

namespace CodeGenerator.Builders.Abstract
{
    public interface IClientWrapperBuilder
    {
        string ReturnVariableName { get; set; }

        IEnumerable<string> GenerateClientWrapperLines(IReadOnlyCollection<MBApiMethodDefinition> methods);
    }
}