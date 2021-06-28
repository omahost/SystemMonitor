using System.ComponentModel;
using System.Windows;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.UI.Interfaces
{
    public interface IMainWindow 
        : ISingletonDependency
    {
        event CancelEventHandler Closing;
        Window GetWindow();
        void Show();
    }
}
