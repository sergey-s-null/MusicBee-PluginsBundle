using System.ComponentModel;
using System.Linq.Expressions;
using Module.Mvvm.Extension.Exceptions;

namespace Module.Mvvm.Extension.Helpers;

public static class ViewModelHelper
{
    public static void RegisterPropertyChangedCallback<TViewModel, TProperty>(
        TViewModel viewModel,
        Expression<Func<TViewModel, TProperty>> propertySelector,
        Action<TViewModel, TProperty> callback)
    {
        if (viewModel is not INotifyPropertyChanged notifier)
        {
            throw new CallbackRegistrationException(
                $"View model {viewModel} does not implement {nameof(INotifyPropertyChanged)}."
            );
        }

        var propertyName = GetPropertyName(propertySelector);
        var selector = propertySelector.Compile();
        notifier.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == propertyName)
            {
                callback(viewModel, selector(viewModel));
            }
        };
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
            throw new CallbackRegistrationException(
                "Could not get property name. Body is not a member expression."
            );
        }

        return memberExpression.Member.Name;
    }
}