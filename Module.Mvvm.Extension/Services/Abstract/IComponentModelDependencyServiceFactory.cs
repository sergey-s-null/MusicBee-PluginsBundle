namespace Module.Mvvm.Extension.Services.Abstract;

public interface IComponentModelDependencyServiceFactory
{
    IScopedComponentModelDependencyService<TDependent> CreateScoped<TDependent>(TDependent dependentObject);
}