using System.ComponentModel;
using System.Linq.Expressions;

namespace Module.Mvvm.Extension.Helpers;

// todo rename to NotifyPropertyChangedHelper
public static class ViewModelHelper
{
    // todo rename to "Handler"
    public static void RegisterPropertyChangedCallback<TViewModel, TProperty>(
        TViewModel viewModel,
        Expression<Func<TViewModel, TProperty>> propertySelector,
        Action<TViewModel, TProperty> callback)
    {
        RegisterPropertyChangedHandler(viewModel, propertySelector, callback, out _);
    }

    public static void RegisterPropertyChangedHandler<TViewModel, TProperty>(
        TViewModel viewModel,
        Expression<Func<TViewModel, TProperty>> propertySelector,
        Action<TViewModel, TProperty> handler,
        out Action unregisterHandler)
    {
        if (viewModel is not INotifyPropertyChanged notifier)
        {
            throw new ArgumentException(
                $"View model {viewModel} does not implement {nameof(INotifyPropertyChanged)}."
            );
        }

        var propertyName = GetPropertyName(propertySelector);
        var selector = propertySelector.Compile();

        PropertyChangedEventHandler wrappedHandler = (_, args) =>
        {
            if (args.PropertyName == propertyName)
            {
                handler(viewModel, selector(viewModel));
            }
        };

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
}