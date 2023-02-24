using System.Reflection;

namespace HackModule.AssemblyBindingRedirect.Services.Abstract;

public interface IAssemblyResolver
{
    Assembly? ResolveHandler(object sender, ResolveEventArgs eventArgs);
}