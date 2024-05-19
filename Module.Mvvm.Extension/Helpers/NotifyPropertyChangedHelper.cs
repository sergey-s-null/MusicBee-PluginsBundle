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
        if (!TryGetPropertyChangedFieldInfoInClassHierarchy(type, out var propertyChangedFieldInfo))
        {
            throw new InvalidOperationException(
                $"Could not get field \"{nameof(INotifyPropertyChanged.PropertyChanged)}\" from type {type}. Value is null."
            );
        }

        return propertyChangedFieldInfo;
    }

    private static bool TryGetPropertyChangedFieldInfoInClassHierarchy(Type rootType, out FieldInfo fieldInfo)
    {
        foreach (var type in EnumerateTypesHierarchy(rootType))
        {
            if (TryGetPropertyChangedField(type, out fieldInfo))
            {
                return true;
            }
        }

        fieldInfo = null!;
        return false;
    }

    private static IEnumerable<Type> EnumerateTypesHierarchy(Type type)
    {
        var typeTemp = type;
        while (typeTemp is not null)
        {
            yield return typeTemp;
            typeTemp = typeTemp.BaseType;
        }
    }

    private static bool TryGetPropertyChangedField(Type type, out FieldInfo fieldInfo)
    {
        var propertyChangedFieldInfo = type.GetField(
            nameof(INotifyPropertyChanged.PropertyChanged),
            BindingFlags.Instance | BindingFlags.NonPublic
        );

        fieldInfo = propertyChangedFieldInfo!;
        return propertyChangedFieldInfo is not null;
    }
}