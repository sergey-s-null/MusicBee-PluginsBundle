using System.Collections;
using System.Linq.Expressions;
using Module.Mvvm.Extension.Helpers;
using PropertyChanged;

namespace Module.Mvvm.Extension.Tests;

public sealed class ViewModelHelperTests
{
    [Test]
    public void PropertyChangedHandlerCalled()
    {
        var vm = new ThirdVM
        {
            Value = 1
        };

        ThirdVM? actualViewModel = null;
        var handlerCallCount = 0;
        var changedValue = 0;
        ViewModelHelper.RegisterPropertyChangedHandler(
            vm,
            x => x.Value,
            (viewModel, value) =>
            {
                actualViewModel = viewModel;
                handlerCallCount++;
                changedValue = value;
            }
        );

        vm.Value = 42;
        Assert.Multiple(() =>
        {
            Assert.That(handlerCallCount, Is.EqualTo(1));
            Assert.That(changedValue, Is.EqualTo(42));
            Assert.That(actualViewModel, Is.Not.Null);
        });
        Assert.That(actualViewModel, Is.EqualTo(vm));
    }

    [Test]
    public void PropertyChangedHandlerNotCalledAfterUnregister()
    {
        var vm = new ThirdVM
        {
            Value = 1
        };

        var handlerCallCount = 0;
        ViewModelHelper.RegisterPropertyChangedHandler(
            vm,
            x => x.Value,
            (_, _) => handlerCallCount++,
            out var unregisterHandler
        );

        vm.Value = 42;
        Assert.That(handlerCallCount, Is.EqualTo(1));

        unregisterHandler();

        vm.Value = 100;
        Assert.That(handlerCallCount, Is.EqualTo(1));
    }

    [TestCaseSource(nameof(InvalidPropertySelectorTestCases))]
    public void ExceptionOnRegisterHandlerWithInvalidSelector<TProperty>(
        Expression<Func<FirstVM, TProperty>> propertySelector)
    {
        var vm = new FirstVM();
        Assert.Throws<ArgumentException>(() =>
            ViewModelHelper.RegisterPropertyChangedHandler(
                vm, propertySelector, (_, _) => { }
            )
        );
    }

    [TestCaseSource(nameof(InvalidPropertySelectorTestCases))]
    public void ExceptionOnRegisterDependencyWithInvalidTargetSelector<TProperty>(
        Expression<Func<FirstVM, TProperty>> propertySelector)
    {
        var vm = new FirstVM();
        Assert.Throws<ArgumentException>(() =>
            ViewModelHelper.RegisterPropertyDependency(
                vm, propertySelector, x => x.Second!.Third!.Value
            )
        );
    }

    [TestCaseSource(nameof(InvalidNestedPropertySelectorTestCases))]
    public void ExceptionOnRegisterDependencyWithInvalidSourceSelector<TProperty>(
        Expression<Func<FirstVM, TProperty>> nestedPropertySelector)
    {
        var vm = new FirstVM();
        Assert.Throws<ArgumentException>(() =>
            ViewModelHelper.RegisterPropertyDependency(
                vm, x => x.Core, nestedPropertySelector
            )
        );
    }

    private static IEnumerable InvalidPropertySelectorTestCases()
    {
        Expression<Func<FirstVM, int>> propertySelector1 = vm => 42;
        Expression<Func<FirstVM, FirstVM>> propertySelector2 = vm => vm;
        Expression<Func<FirstVM, ThirdVM>> propertySelector3 = vm => vm.Second!.Third!;
        yield return new TestCaseData(propertySelector1);
        yield return new TestCaseData(propertySelector2);
        yield return new TestCaseData(propertySelector3);
    }

    private static IEnumerable InvalidNestedPropertySelectorTestCases()
    {
        Expression<Func<FirstVM, int>> propertySelector1 = vm => 42;
        Expression<Func<FirstVM, FirstVM>> propertySelector2 = vm => vm;
        Expression<Func<FirstVM, SecondVM>> propertySelector3 = vm => vm.Second!;
        yield return new TestCaseData(propertySelector1);
        yield return new TestCaseData(propertySelector2);
        yield return new TestCaseData(propertySelector3);
    }

    [AddINotifyPropertyChangedInterface]
    public sealed class FirstVM
    {
        public int Core { get; set; }
        public SecondVM? Second { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public sealed class SecondVM
    {
        public ThirdVM? Third { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public sealed class ThirdVM
    {
        public int Value { get; set; }
    }
}