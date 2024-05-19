using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using CommunityToolkit.Diagnostics;

namespace Module.Mvvm.Extension.Helpers;

public static class ViewModelHelper
{
    public static void RegisterPropertyChangedHandler<TViewModel, TProperty>(
        TViewModel viewModel,
        Expression<Func<TViewModel, TProperty>> propertySelector,
        Action<TViewModel, TProperty> handler)
    {
        RegisterPropertyChangedHandler(viewModel, propertySelector, handler, out _);
    }

    public static void RegisterPropertyChangedHandler<TViewModel, TProperty>(
        TViewModel viewModel,
        Expression<Func<TViewModel, TProperty>> propertyExpression,
        Action<TViewModel, TProperty> handler,
        out Action unregisterHandler)
    {
        if (viewModel is not INotifyPropertyChanged)
        {
            throw new ArgumentException(
                $"View model {viewModel} does not implement {nameof(INotifyPropertyChanged)}."
            );
        }

        var propertyName = GetPropertyName(propertyExpression);
        var propertySelector = propertyExpression.Compile();

        RegisterPropertyChangedHandler(viewModel, propertyName, propertySelector, handler, out unregisterHandler);
    }

    public static void RegisterPropertyChangedHandler<TViewModel>(
        TViewModel viewModel,
        string propertyName,
        Action<TViewModel, object> handler,
        out Action unregisterHandler)
    {
        Guard.IsNotNull(viewModel);

        var type = viewModel.GetType();
        var getMethod = GetPropertyGetMethod(type, propertyName);

        RegisterPropertyChangedHandler(
            viewModel,
            propertyName,
            x => getMethod.Invoke(x, new object[] { }),
            handler,
            out unregisterHandler
        );
    }

    private static void RegisterPropertyChangedHandler<TViewModel, TProperty>(
        TViewModel viewModel,
        string propertyName,
        Func<TViewModel, TProperty> propertySelector,
        Action<TViewModel, TProperty> handler,
        out Action unregisterHandler)
    {
        PropertyChangedEventHandler wrappedHandler = (_, args) =>
        {
            if (args.PropertyName == propertyName)
            {
                handler(viewModel, propertySelector(viewModel));
            }
        };

        var notifier = (INotifyPropertyChanged)viewModel!;

        notifier.PropertyChanged += wrappedHandler;
        unregisterHandler = () => notifier.PropertyChanged -= wrappedHandler;
    }

    private static string GetPropertyName<TViewModel, TProperty>(
        Expression<Func<TViewModel, TProperty>> propertySelector)
    {
        var body = propertySelector.Body;
        while (body is UnaryExpression unaryExpression)
        {
            body = unaryExpression.Operand;
        }

        if (body is not MemberExpression memberExpression)
        {
            throw new ArgumentException(
                "Could not get property name. Body is not a member expression.",
                nameof(propertySelector)
            );
        }

        if (memberExpression.Expression is not ParameterExpression)
        {
            throw new ArgumentException(
                "Property selector is invalid.",
                nameof(propertySelector)
            );
        }

        return memberExpression.Member.Name;
    }

    private static MethodInfo GetPropertyGetMethod(Type type, string propertyName)
    {
        var propertyInfo = type.GetProperty(propertyName);
        if (propertyInfo is null)
        {
            throw new InvalidOperationException(
                $"Could not get property info by name \"{propertyName}\" on type {type}. Value is null."
            );
        }

        return propertyInfo.GetMethod;
    }
}