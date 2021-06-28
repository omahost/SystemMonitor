using Prism.Commands;
using Prism.Mvvm;
using SystemMonitor.Infrastructure.Interfaces.Dialogs;
using SystemMonitor.Control.Settings.Interfaces;
using SystemMonitor.Control.Orders.Interfaces;

namespace SystemMonitor.Control.Toolbar
{
    public class ToolbarControlViewModel : BindableBase
    {
        private readonly IDialogManager _dialogManager;

        public ToolbarControlViewModel(
            IDialogManager dialogManager
            )
        {
            _dialogManager = dialogManager;

            SettingsCommand = new DelegateCommand(OpenSettings);
            ShowOrdersCommand = new DelegateCommand(ShowOrders);
        }

        public DelegateCommand SettingsCommand { get; }
        private void OpenSettings()
        {
            _dialogManager.ShowDialog<ISettingsView>();
        }

        public DelegateCommand ShowOrdersCommand { get; }
        private void ShowOrders()
        {
            _dialogManager.ShowDialog<IOrdersView>();
        }
    }
}
