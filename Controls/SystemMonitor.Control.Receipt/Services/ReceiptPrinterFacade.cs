using SystemMonitor.Control.Receipt.Interfaces;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Infrastructure.Interfaces.Dialogs;

namespace SystemMonitor.Control.Receipt.Services
{
    public class ReceiptPrinterFacade : IReceiptPrinterFacade
    {
        private readonly IDialogManager _dialogManager;

        public ReceiptPrinterFacade(
            IDialogManager dialogManager
            )
        {
            _dialogManager = dialogManager;
        }

        public void Print(IDeviceInfo deviceInfo)
        {
            var dialog = _dialogManager.CreateDialog<IReceiptView, ReceiptViewModel>((viewModel) => 
            {
                viewModel.Device = deviceInfo;
            });
            dialog.ShowDialog();
        }
    }
}
