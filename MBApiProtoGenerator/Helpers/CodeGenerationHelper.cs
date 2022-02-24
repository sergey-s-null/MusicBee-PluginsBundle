using System.Collections.Generic;

namespace MBApiProtoGenerator.Helpers
{
    public static class CodeGenerationHelper
    {
        public static IEnumerable<string> WrapWithNamespace(IEnumerable<string> lines, string @namespace)
        {
            return WrapBlock(lines, $"namespace {@namespace}");
        }

        public static IEnumerable<string> WrapBlock(IEnumerable<string> lines, string? headerLine = null)
        {
            if (headerLine is not null)
            {
                yield return headerLine;
            }

            yield return "{";
            foreach (var line in lines)
            {
                yield return line.Indented();
            }

            yield return "}";
        }
    }
}