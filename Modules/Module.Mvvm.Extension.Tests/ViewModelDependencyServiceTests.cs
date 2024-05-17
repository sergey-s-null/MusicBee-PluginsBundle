using System.Collections;
using System.Linq.Expressions;
using Module.Mvvm.Extension.Services.Abstract;
using Module.Mvvm.Extension.Tests.ViewModels;

namespace Module.Mvvm.Extension.Tests;

public class ViewModelDependencyServiceTests
{
    private IViewModelDependencyService? _viewModelDependencyService;

    [SetUp]
    public void Setup()
    {
        _viewModelDependencyService = null;
    }

    [TestCaseSource(nameof(InvalidDependentPropertiesTestCases))]
    public void ExceptionOnRegisterDependencyWithInvalidDependentProperty<TProperty>(
        Expression<Func<DependentVM, TProperty>> invalidDependentProperty)
    {
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        Assert.Throws<ArgumentException>(() =>
            _viewModelDependencyService!.RegisterDependency(
                dependent,
                invalidDependentProperty,
                dependency,
                x => x.Number,
                out _
            )
        );
    }

    [TestCaseSource(nameof(InvalidDependencyPropertiesTestCases))]
    public void ExceptionOnRegisterDependencyWithInvalidDependencyProperty<TProperty>(
        Expression<Func<DependencyVM, TProperty>> invalidDependencyProperty)
    {
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        Assert.Throws<ArgumentException>(() =>
            _viewModelDependencyService!.RegisterDependency(
                dependent,
                x => x.Value,
                dependency,
                invalidDependencyProperty,
                out _
            )
        );
    }

    private static IEnumerable InvalidDependentPropertiesTestCases()
    {
        Expression<Func<DependentVM, int>> property1 = vm => 42;
        Expression<Func<DependentVM, DependentVM>> property2 = vm => vm;
        Expression<Func<DependentVM, string>> property3 = vm => vm.Child!.Text;
        yield return new TestCaseData(property1);
        yield return new TestCaseData(property2);
        yield return new TestCaseData(property3);
    }

    private static IEnumerable InvalidDependencyPropertiesTestCases()
    {
        Expression<Func<DependencyVM, int>> property1 = vm => 42;
        Expression<Func<DependencyVM, DependencyVM>> property2 = vm => vm;
        yield return new TestCaseData(property1);
        yield return new TestCaseData(property2);
    }
}