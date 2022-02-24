using System.Collections.Generic;
using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders.ServiceImplBuilder.Abstract
{
    public interface IMethodBuilder
    {
        IEnumerable<string> GenerateMethodLines(MBApiMethodDefinition method);
    }
}