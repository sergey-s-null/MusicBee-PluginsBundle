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

        /// <summary>
        /// Возвращает строку вызова метода, как если бы его вызывали _mbApi....
        /// Clear означает, что у метода не указан объект в начале и нет ";" в конце
        /// </summary>
        /// <param name="method"></param>
        /// <param name="withVars"></param>
        /// <returns></returns>
        string GetClearMethodCall(MBApiMethodDefinition method, bool withVars);
    }
}