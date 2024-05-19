using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using CommunityToolkit.Diagnostics;
using Module.Mvvm.Extension.Helpers;
using Module.Mvvm.Extension.Services.Abstract;
using PropertyDescriptor = Module.Mvvm.Extension.Entities.PropertyDescriptor;

namespace Module.Mvvm.Extension.Services;

public sealed class ViewModelDependencyService : IViewModelDependencyService
{
    public void RegisterDependency<TDependent, TDependentProperty, TDependency, TDependencyProperty>(
        TDependent dependentViewModel,
        Expression<Func<TDependent, TDependentProperty>> dependentProperty,
        TDependency dependencyViewModel,
        Expression<Func<TDependency, TDependencyProperty>> dependencyProperty,
        out Action unregisterDependency)
    {
        Guard.IsNotNull(dependentViewModel);
        Guard.IsNotNull(dependentProperty);
        Guard.IsNotNull(dependencyViewModel);
        Guard.IsNotNull(dependencyProperty);

        var dependentPropertyDescriptor = BuildSinglePropertyDescriptor(dependentProperty);
        var dependencyPropertyDescriptors = BuildPropertyDescriptors(dependencyProperty);

        object viewModel = dependencyViewModel;
        foreach (var dependencyPropertyDescriptor in dependencyPropertyDescriptors)
        {
            if (viewModel is null)
            {
                break;
            }

            ViewModelHelper.RegisterPropertyChangedHandler(
                viewModel,
                dependencyPropertyDescriptor.Name,
                (_, _) => { RaisePropertyChangedEvent(dependentViewModel, dependentPropertyDescriptor.Name); },
                out _
            );

            viewModel = dependencyPropertyDescriptor.ValueSelector(viewModel);
        }

        unregisterDependency = null;
        // throw new NotImplementedException();
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

    private static void RaisePropertyChangedEvent(object viewModel, string propertyName)
    {
        var propertyChangedFieldInfo = GetPropertyChangedFieldInfo(viewModel);
        var eventDelegate = (MulticastDelegate)propertyChangedFieldInfo.GetValue(viewModel);
        if (eventDelegate is null)
        {
            return;
        }

        foreach (var handler in eventDelegate.GetInvocationList())
        {
            var args = new PropertyChangedEventArgs(propertyName);
            handler.Method.Invoke(handler.Target, new[] { viewModel, args });
        }
    }

    private static FieldInfo GetPropertyChangedFieldInfo(object viewModel)
    {
        var type = viewModel.GetType();
        var propertyChangedFieldInfo = type.GetField(
            nameof(INotifyPropertyChanged.PropertyChanged),
            BindingFlags.Instance | BindingFlags.NonPublic
        );

        if (propertyChangedFieldInfo is null)
        {
            throw new InvalidOperationException(
                $"Could not get field \"{nameof(INotifyPropertyChanged.PropertyChanged)}\" from type {type}. Value is null."
            );
        }

        return propertyChangedFieldInfo;
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
}