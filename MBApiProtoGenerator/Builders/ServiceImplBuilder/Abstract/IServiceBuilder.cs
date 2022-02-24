using System.Collections.Generic;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract
{
    public interface IServiceBuilder
    {
        IEnumerable<string> GenerateServiceLines(IReadOnlyCollection<MBApiMethodDefinition> methods);
    }
}