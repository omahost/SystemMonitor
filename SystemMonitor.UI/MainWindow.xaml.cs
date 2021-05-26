using System.Windows;
using SystemMonitor.UI.Interfaces;

namespace SystemMonitor.UI
{
    public partial class MainWindow 
        : Window
        , IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Window GetWindow() => this;
    }
}
