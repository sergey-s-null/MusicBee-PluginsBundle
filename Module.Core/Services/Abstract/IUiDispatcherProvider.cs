using System.Windows.Threading;

namespace Module.Core.Services.Abstract;

public interface IUiDispatcherProvider
{
    Dispatcher Dispatcher { get; }
}