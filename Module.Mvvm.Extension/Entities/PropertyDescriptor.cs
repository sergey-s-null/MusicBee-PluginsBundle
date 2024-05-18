namespace Module.Mvvm.Extension.Entities;

public sealed record PropertyDescriptor(
    string Name,
    Func<object, object> ValueSelector
);