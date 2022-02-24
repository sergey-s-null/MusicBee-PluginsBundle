using System.Collections.Generic;
using CodeGenerator.Models;

namespace CodeGenerator.Builders.Abstract
{
    public interface IInterfaceBuilder
    {
        public IReadOnlyCollection<string>? ImportNamespaces { get; set; }
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string? BaseInterface { get; set; }
        
        IEnumerable<string> GenerateInterfaceLines(IReadOnlyCollection<MBApiMethodDefinition> methods);
    }
}