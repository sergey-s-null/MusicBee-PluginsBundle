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
        var handlerCalledCount = 0;
        var changedValue = 0;
        ViewModelHelper.RegisterPropertyChangedHandler(
            vm,
            x => x.Value,
            (viewModel, value) =>
            {
                actualViewModel = viewModel;
                handlerCalledCount++;
                changedValue = value;
            }
        );

        vm.Value = 42;
        Assert.Multiple(() =>
        {
            Assert.That(handlerCalledCount, Is.EqualTo(1));
            Assert.That(changedValue, Is.EqualTo(42));
            Assert.That(actualViewModel, Is.Not.Null);
        });
        Assert.That(actualViewModel, Is.EqualTo(vm));
    }

    [Test]
    public void PropertyChangedHandlerCalledWhenRegisteredByPropertyName()
    {
        // ARRANGE
        var vm = new NodeVM
        {
            Value = 1
        };

        NodeVM? actualViewModel = null;
        var handlerCalledCount = 0;
        object? changedValue = null;
        ViewModelHelper.RegisterPropertyChangedHandler(
            vm,
            nameof(NodeVM.Value),
            (viewModel, value) =>
            {
                actualViewModel = viewModel;
                handlerCalledCount++;
                changedValue = value;
            },
            out _
        );

        // ACT
        vm.Value = 42;

        // ASSERT
        Assert.Multiple(() =>
        {
            Assert.That(handlerCalledCount, Is.EqualTo(1));
            Assert.That(changedValue, Is.EqualTo(42));
            Assert.That(actualViewModel, Is.Not.Null);
        });
        Assert.That(actualViewModel, Is.EqualTo(vm));
    }

    [Test]
    public void PropertyChangedHandlerCalledWhenViewModelPassedAsObject()
    {
        // ARRANGE
        var vm = new NodeVM
        {
            Value = 1
        };

        var handlerCalledCount = 0;
        ViewModelHelper.RegisterPropertyChangedHandler(
            (object)vm,
            nameof(NodeVM.Value),
            (_, _) => handlerCalledCount++,
            out _
        );

        // ACT
        vm.Value = 42;

        // ASSERT
        Assert.That(handlerCalledCount, Is.EqualTo(1));
    }

    [Test]
    public void PropertyChangedHandlerNotCalledAfterUnregister()
    {
        var vm = new NodeVM
        {
            Value = 1
        };

        var handlerCalledCount = 0;
        ViewModelHelper.RegisterPropertyChangedHandler(
            vm,
            x => x.Value,
            (_, _) => handlerCalledCount++,
            out var unregisterHandler
        );

        vm.Value = 42;
        Assert.That(handlerCalledCount, Is.EqualTo(1));

        unregisterHandler();

        vm.Value = 100;
        Assert.That(handlerCalledCount, Is.EqualTo(1));
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