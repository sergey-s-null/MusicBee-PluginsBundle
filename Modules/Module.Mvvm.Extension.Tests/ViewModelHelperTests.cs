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

    [Test]
    public void ExceptionThrownOnValueInPropertySelector()
    {
        var vm = new FirstVM();
        Assert.Throws<ArgumentException>(() =>
            ViewModelHelper.RegisterPropertyChangedHandler(
                vm, x => 42, (_, _) => { }
            )
        );
    }

    [Test]
    public void ExceptionThrownOnViewModelInPropertySelector()
    {
        var vm = new FirstVM();
        Assert.Throws<ArgumentException>(() =>
            ViewModelHelper.RegisterPropertyChangedHandler(
                vm, x => x, (_, _) => { }
            )
        );
    }

    [Test]
    public void ExceptionThrownOnComplexPropertySelector()
    {
        var vm = new FirstVM();
        Assert.Throws<ArgumentException>(() =>
            ViewModelHelper.RegisterPropertyChangedHandler(
                vm, x => x.Second!.Third, (_, _) => { }
            )
        );
    }

    [AddINotifyPropertyChangedInterface]
    private sealed class FirstVM
    {
        public SecondVM? Second { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    private sealed class SecondVM
    {
        public ThirdVM? Third { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    private sealed class ThirdVM
    {
        public int Value { get; set; }
    }
}