using System.Windows.Threading;
using Module.Core.Services.Abstract;

namespace Module.Core.Services;

public sealed class UiDispatcherProvider : IUiDispatcherProvider
{
    public Dispatcher Dispatcher { get; }

    public UiDispatcherProvider(Dispatcher dispatcher)
    {
        Dispatcher = dispatcher;
    }
}