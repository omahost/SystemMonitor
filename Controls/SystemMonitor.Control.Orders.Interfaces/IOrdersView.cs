using SystemMonitor.Infrastructure.Interfaces.Dialogs;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Control.Orders.Interfaces
{
    public interface IOrdersView
        : IDialogView
        , ISingletonDependency
    {
    }
}
