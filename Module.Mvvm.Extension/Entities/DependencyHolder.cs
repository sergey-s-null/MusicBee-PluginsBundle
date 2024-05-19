using System.Linq.Expressions;
using System.Reflection;
using CommunityToolkit.Diagnostics;
using Module.Mvvm.Extension.Helpers;

namespace Module.Mvvm.Extension.Entities;

public sealed class DependencyHolder<TDependent, TDependentProperty, TDependency, TDependencyProperty>
{
    private readonly TDependent _dependentViewModel;
    private readonly TDependency _dependencyViewModel;
    private readonly PropertyDescriptor _dependentPropertyDescriptor;
    private readonly IReadOnlyList<PropertyDescriptor> _dependencyPropertyDescriptors;
    private readonly Stack<Action> _unregisters = new();

    private bool _isRegistered;

    public DependencyHolder(
        TDependent dependentViewModel,
        Expression<Func<TDependent, TDependentProperty>> dependentProperty,
        TDependency dependencyViewModel,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty)
    {
        Guard.IsNotNull(dependentViewModel);
        Guard.IsNotNull(dependentProperty);
        Guard.IsNotNull(dependencyViewModel);
        Guard.IsNotNull(dependencyProperty);

        _dependentViewModel = dependentViewModel;
        _dependencyViewModel = dependencyViewModel;
        _dependentPropertyDescriptor = BuildSinglePropertyDescriptor(dependentProperty);
        _dependencyPropertyDescriptors = BuildPropertyDescriptors(dependencyProperty);
    }

    public void Register()
    {
        if (_isRegistered)
        {
            throw new InvalidOperationException("Already registered.");
        }

        RegisterHandlersUnder(_dependencyViewModel, 0);

        _isRegistered = true;
    }

    public void Unregister()
    {
        if (!_isRegistered)
        {
            throw new InvalidOperationException("Already unregistered.");
        }

        while (_unregisters.Count > 0)
        {
            var unregister = _unregisters.Pop();
            unregister();
        }

        _isRegistered = false;
    }

    private static PropertyDescriptor BuildSinglePropertyDescriptor<T, TProperty>(
        Expression<Func<T, TProperty>> propertyExpression)
    {
        var expression = propertyExpression.Body;

        if (expression.NodeType != ExpressionType.MemberAccess
            || expression is not MemberExpression memberExpression)
        {
            throw new ArgumentException(
                $"Expression {expression} is not member expression.",
                nameof(propertyExpression)
            );
        }

        var descriptor = BuildPropertyDescriptor(memberExpression.Member);

        expression = memberExpression.Expression;
        if (expression.NodeType != ExpressionType.Parameter)
        {
            throw new ArgumentException(
                $"Member expression {memberExpression} has invalid expression. " +
                $"Expected expression type is Parameter, actual is {expression.NodeType}.",
                nameof(propertyExpression)
            );
        }

        return descriptor;
    }

    private static IReadOnlyList<PropertyDescriptor> BuildPropertyDescriptors<T, TProperty>(
        Expression<Func<T, TProperty>> propertyExpression)
    {
        var descriptors = EnumeratePropertyDescriptors(propertyExpression.Body).ToList();
        descriptors.Reverse();
        return descriptors;
    }


    private static IEnumerable<PropertyDescriptor> EnumeratePropertyDescriptors(Expression expression)
    {
        while (true)
        {
            if (expression.NodeType == ExpressionType.Parameter)
            {
                break;
            }

            if (expression.NodeType != ExpressionType.MemberAccess
                || expression is not MemberExpression memberExpression)
            {
                throw new ArgumentException($"Expression {expression} is not member expression.");
            }

            yield return BuildPropertyDescriptor(memberExpression.Member);

            expression = memberExpression.Expression;
        }
    }

    private static PropertyDescriptor BuildPropertyDescriptor(MemberInfo member)
    {
        if (member.MemberType != MemberTypes.Property)
        {
            throw new ArgumentException($"Member {member} is not property.");
        }

        var declaringType = GetDeclaringType(member);

        var propertyName = member.Name;
        var propertyInfo = GetPropertyInfo(declaringType, propertyName);
        var propertyGetMethod = propertyInfo.GetMethod;

        return new PropertyDescriptor(
            propertyName,
            x => propertyGetMethod.Invoke(x, new object[] { })
        );
    }

    private static Type GetDeclaringType(MemberInfo member)
    {
        var declaringType = member.DeclaringType;
        if (declaringType is null)
        {
            throw new InvalidOperationException(
                $"Declaring type of member {member} is null."
            );
        }

        return declaringType;
    }

    private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
    {
        var propertyInfo = type.GetProperty(propertyName);
        if (propertyInfo is null)
        {
            throw new InvalidOperationException(
                $"Could not get property info by property name \"{propertyName}\" from type {type}. Value is null."
            );
        }

        return propertyInfo;
    }

    private void RegisterHandlersUnder(object? root, int startIndex)
    {
        var item = root;
        for (var i = startIndex; i < _dependencyPropertyDescriptors.Count; i++)
        {
            if (item is null)
            {
                break;
            }

            var descriptor = _dependencyPropertyDescriptors[i];

            RegisterPropertyChangedHandler(item, descriptor.Name, i, out var unregisterHandler);
            _unregisters.Push(unregisterHandler);

            item = descriptor.ValueSelector(item);
        }
    }

    private void RegisterPropertyChangedHandler(
        object item,
        string propertyName,
        int propertyIndex,
        out Action unregisterHandler)
    {
        ViewModelHelper.RegisterPropertyChangedHandler(
            item,
            propertyName,
            (_, value) =>
            {
                UnregisterUpToExclusively(propertyIndex);
                RegisterHandlersUnder(value, propertyIndex + 1);
                RaiseDependentPropertyChanged();
            },
            out unregisterHandler
        );
    }

    private void UnregisterUpToExclusively(int propertyIndex)
    {
        while (_unregisters.Count > propertyIndex + 1)
        {
            var unregister = _unregisters.Pop();
            unregister();
        }
    }

    private void RaiseDependentPropertyChanged()
    {
        NotifyPropertyChangedHelper.RaisePropertyChangedEvent(
            _dependentViewModel!,
            _dependentPropertyDescriptor.Name
        );
    }
}