using System.Windows;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.UI.Interfaces
{
    public interface IMainWindow 
        : ISingletonDependency
    {
        Window GetWindow();
        void Show();
    }
}
