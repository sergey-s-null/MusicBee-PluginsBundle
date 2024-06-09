using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using Module.Mvvm.Extension.Services;
using Module.Mvvm.Extension.Services.Abstract;
using Module.Mvvm.Extension.Tests.ViewModels;

// ReSharper disable SuspiciousTypeConversion.Global

namespace Module.Mvvm.Extension.Tests;

public class ComponentModelDependencyServiceTests
{
    private IComponentModelDependencyService? _componentModelDependencyService;

    [SetUp]
    public void Setup()
    {
        _componentModelDependencyService = new ComponentModelDependencyService();
    }

    [Test]
    public void RegisteredSuccessfully()
    {
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Child!.Text,
            out _
        );
    }

    [Test]
    public void EventRaisedWhenSimpleDependencyRegistered()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Number,
            out _
        );

        var dependentInterface = (INotifyPropertyChanged)(object)dependent;
        var eventRaisedCount = 0;
        object? actualSender = null;
        string? actualPropertyName = null;
        dependentInterface.PropertyChanged += (sender, args) =>
        {
            eventRaisedCount++;
            actualSender = sender;
            actualPropertyName = args.PropertyName;
        };

        // ACT
        dependency.Number = 42;

        // ASSERT
        Assert.Multiple(() =>
        {
            Assert.That(eventRaisedCount, Is.EqualTo(1));
            Assert.That(actualSender, Is.EqualTo(dependent));
            Assert.That(actualPropertyName, Is.EqualTo(nameof(DependentVM.Value)));
        });
    }

    [Test]
    public void EventRaisedWhenDeepPropertyChanged()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM
        {
            Child = new ChildVM
            {
                Child = new ChildVM()
            }
        };

        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Child!.Child!.Text,
            out _
        );

        var dependentInterface = (INotifyPropertyChanged)(object)dependent;
        var eventRaisedCount = 0;
        dependentInterface.PropertyChanged += (_, _) => eventRaisedCount++;

        // ACT
        dependency.Child.Child.Text = "some text";

        // ASSERT
        Assert.That(eventRaisedCount, Is.EqualTo(1));
    }

    [Test]
    public void EventRaisedWhenPropertyChangedInMiddleOfPath()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Child!.Text,
            out _
        );

        var dependentInterface = (INotifyPropertyChanged)(object)dependent;
        var eventRaisedCount = 0;
        dependentInterface.PropertyChanged += (_, _) => eventRaisedCount++;

        // ACT
        dependency.Child = new ChildVM();

        // ASSERT
        Assert.That(eventRaisedCount, Is.EqualTo(1));
    }

    [Test]
    public void EventRaisedWhenPropertyChangedInNewlyAddedObjectInMiddleOfPath()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Child!.Text,
            out _
        );

        var dependentInterface = (INotifyPropertyChanged)(object)dependent;
        var eventRaisedCount = 0;
        dependentInterface.PropertyChanged += (_, _) => eventRaisedCount++;

        // ACT
        dependency.Child = new ChildVM();
        dependency.Child.Text = "some text";

        // ASSERT
        Assert.That(eventRaisedCount, Is.EqualTo(2));
    }

    [Test]
    public void EventRaisedWhenDependentPropertyIsPrivate()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        dependent.RegisterIntervalValueDependency(
            _componentModelDependencyService!,
            dependency,
            x => x.Child
        );

        var dependentInterface = (INotifyPropertyChanged)(object)dependent;
        var eventRaisedCount = 0;
        dependentInterface.PropertyChanged += (_, _) => eventRaisedCount++;

        // ACT
        dependency.Child = new ChildVM();

        // ASSERT
        Assert.That(eventRaisedCount, Is.EqualTo(1));
    }

    [Test]
    public void EventRaisedWhenDependencyPropertyIsPrivate()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        dependency.RegisterDependentOnInternalNumberProperty(
            _componentModelDependencyService!,
            dependent,
            x => x.Value
        );

        var dependentInterface = (INotifyPropertyChanged)(object)dependent;
        var eventRaisedCount = 0;
        dependentInterface.PropertyChanged += (_, _) => eventRaisedCount++;

        // ACT
        dependency.ChangeInternalNumber(42);

        // ASSERT
        Assert.That(eventRaisedCount, Is.EqualTo(1));
    }

    [Test]
    public void EventNotRaisedWhenUnregistered()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Number,
            out var unregisterDependency
        );

        var dependentInterface = (INotifyPropertyChanged)(object)dependent;
        var eventRaisedCount = 0;
        dependentInterface.PropertyChanged += (_, _) => eventRaisedCount++;

        // ACT
        unregisterDependency();
        dependency.Number = 42;

        // ASSERT
        Assert.That(eventRaisedCount, Is.EqualTo(0));
    }

    [Test]
    public void EventNotRaisedWhenNeighboringPropertyChanged()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Child,
            out _
        );

        var dependentInterface = (INotifyPropertyChanged)(object)dependent;
        var eventRaisedCount = 0;
        dependentInterface.PropertyChanged += (_, _) => eventRaisedCount++;

        // ACT
        dependency.Number = 42;

        // ASSERT
        Assert.That(eventRaisedCount, Is.EqualTo(0));
    }

    [Test]
    public void EventNotRaisedWhenPropertyChangedInObjectRemovedFromDependencyChain()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var previousChild = new ChildVM();
        var dependency = new DependencyVM
        {
            Child = previousChild
        };

        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Child!.Text,
            out _
        );

        dependency.Child.Text = "some text";

        dependency.Child = null;

        var dependentInterface = (INotifyPropertyChanged)(object)dependent;
        var eventRaisedCount = 0;
        dependentInterface.PropertyChanged += (_, _) => eventRaisedCount++;

        // ACT
        previousChild.Text = "another text";

        // ASSERT
        Assert.That(eventRaisedCount, Is.EqualTo(0));
    }

    [TestCaseSource(nameof(InvalidDependentPropertiesTestCases))]
    public void ExceptionOnRegisterDependencyWithInvalidDependentProperty<TProperty>(
        Expression<Func<DependentVM, TProperty>> invalidDependentProperty)
    {
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        Assert.Throws<ArgumentException>(() =>
            _componentModelDependencyService!.RegisterDependency(
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
            _componentModelDependencyService!.RegisterDependency(
                dependent,
                x => x.Value,
                dependency,
                invalidDependencyProperty,
                out _
            )
        );
    }

    [Test]
    public void RegisteredWhenDependencyNotExplicitlyImplementInterface()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM();

        // ACT
        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Container!.Value,
            out _
        );
    }

    [Test]
    public void ExceptionOnRegisterDependencyWithoutInterfaceImplemented()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM { Container = new ValueContainer() };

        // ACT & ASSERT
        Assert.Throws<InvalidOperationException>(() =>
            _componentModelDependencyService!.RegisterDependency(
                dependent,
                x => x.Value,
                dependency,
                x => x.Container!.Value,
                out _
            )
        );
    }

    [Test]
    public void ExceptionOnSetValueWithoutInterfaceImplemented()
    {
        // ARRANGE
        var dependent = new DependentVM();
        var dependency = new DependencyVM
        {
            Container = new ValueContainerWithPropertyChanged()
        };

        _componentModelDependencyService!.RegisterDependency(
            dependent,
            x => x.Value,
            dependency,
            x => x.Container!.Value,
            out _
        );

        // ACT & ASSERT
        Assert.Throws<InvalidOperationException>(() =>
            dependency.Container = new ValueContainer { Value = 42 }
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