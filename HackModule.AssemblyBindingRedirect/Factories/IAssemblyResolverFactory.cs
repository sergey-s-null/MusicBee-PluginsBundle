using HackModule.AssemblyBindingRedirect.Services.Abstract;

namespace HackModule.AssemblyBindingRedirect.Factories
{
    public interface IAssemblyResolverFactory
    {
        IAssemblyResolver Create(string assembliesDirectory);
    }
}