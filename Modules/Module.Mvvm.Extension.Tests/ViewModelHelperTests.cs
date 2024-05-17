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
        var vm = new NodeVM
        {
            Value = 1
        };

        NodeVM? actualViewModel = null;
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
        var vm = new NodeVM
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
        Expression<Func<NodeVM, TProperty>> propertySelector)
    {
        var vm = new NodeVM();
        Assert.Throws<ArgumentException>(() =>
            ViewModelHelper.RegisterPropertyChangedHandler(
                vm, propertySelector, (_, _) => { }
            )
        );
    }

    private static IEnumerable InvalidPropertySelectorTestCases()
    {
        Expression<Func<NodeVM, int>> propertySelector1 = vm => 42;
        Expression<Func<NodeVM, NodeVM>> propertySelector2 = vm => vm;
        Expression<Func<NodeVM, int>> propertySelector3 = vm => vm.Child!.Value;
        yield return new TestCaseData(propertySelector1);
        yield return new TestCaseData(propertySelector2);
        yield return new TestCaseData(propertySelector3);
    }

    [AddINotifyPropertyChangedInterface]
    public sealed class NodeVM
    {
        public int Value { get; set; }
        public NodeVM? Child { get; set; }
    }
}