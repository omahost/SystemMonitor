using SystemMonitor.Infrastructure.Interfaces.Dialogs;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Control.Receipt.Interfaces
{
    public interface IReceiptView 
        : ISingletonDependency
        , IDialogView
    {
    }
}
