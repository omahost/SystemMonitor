using SystemMonitor.Infrastructure.Interfaces.Dialogs;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Control.Settings.Interfaces
{
    public interface ISettingsView 
        : IDialogView
        , ISingletonDependency
    {
    }
}
