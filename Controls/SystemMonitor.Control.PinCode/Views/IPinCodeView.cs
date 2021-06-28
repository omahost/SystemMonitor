using SystemMonitor.Infrastructure.Interfaces.Dialogs;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Control.PinCode
{
    public interface IPinCodeView 
        : IDialogView
        , ISingletonDependency 
    {
    }
}
