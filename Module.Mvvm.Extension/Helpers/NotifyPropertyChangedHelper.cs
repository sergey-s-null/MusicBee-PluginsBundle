using System.ComponentModel;
using System.Reflection;

namespace Module.Mvvm.Extension.Helpers;

public static class NotifyPropertyChangedHelper
{
    public static void RaisePropertyChangedEvent(object sender, string propertyName)
    {
        var propertyChangedFieldInfo = GetPropertyChangedFieldInfo(sender);
        var eventDelegate = (MulticastDelegate)propertyChangedFieldInfo.GetValue(sender);
        if (eventDelegate is null)
        {
            return;
        }

        foreach (var handler in eventDelegate.GetInvocationList())
        {
            var args = new PropertyChangedEventArgs(propertyName);
            handler.Method.Invoke(handler.Target, new[] { sender, args });
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
}