using System.Windows.Controls;
using SystemMonitor.Control.Settings.Interfaces;

namespace SystemMonitor.Control.Settings
{
    public partial class SettingsView 
        : UserControl
        , ISettingsView
    {
        public SettingsView()
        {
            InitializeComponent();
        }
    }
}
