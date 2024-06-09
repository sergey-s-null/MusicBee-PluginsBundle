using Module.Mvvm.Extension.Helpers;
using Module.Mvvm.Extension.Tests.ViewModels;

namespace Module.Mvvm.Extension.Tests;

public sealed class NotifyPropertyChangedHelperTests
{
    [Test]
    public void EventRaised()
    {
        // ARRANGE
        var vm = new BaseVM();

        var handlerCalledCount = 0;
        ViewModelHelper.RegisterPropertyChangedHandler(
            vm,
            x => x.Value,
            (_, _) => handlerCalledCount++
        );

        // ACT
        NotifyPropertyChangedHelper.RaisePropertyChangedEvent(vm, nameof(BaseVM.Value));

        // ASSERT
        Assert.That(handlerCalledCount, Is.EqualTo(1));
    }

    [Test]
    public void EventRaisedInDerivedClass()
    {
        // ARRANGE
        var vm = new DerivedVM();

        var handlerCalledCount = 0;
        ViewModelHelper.RegisterPropertyChangedHandler(
            vm,
            x => x.Text,
            (_, _) => handlerCalledCount++
        );

        // ACT
        NotifyPropertyChangedHelper.RaisePropertyChangedEvent(vm, nameof(DerivedVM.Text));

        // ASSERT
        Assert.That(handlerCalledCount, Is.EqualTo(1));
    }
}