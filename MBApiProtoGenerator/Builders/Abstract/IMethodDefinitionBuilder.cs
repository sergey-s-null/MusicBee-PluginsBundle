using MBApiProtoGenerator.Models;

namespace MBApiProtoGenerator.Builders.Abstract
{
    public interface IMethodDefinitionBuilder
    {
        /// <summary>
        /// Clear означает, что нет префиксов и ";" в конце.
        /// Есть возвращаемый тип, имя метода, входные параметры, выходные параметры
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        string GetClearMethodDefinition(MBApiMethodDefinition method);
    }
}