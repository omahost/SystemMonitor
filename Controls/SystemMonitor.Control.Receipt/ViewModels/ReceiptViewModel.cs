using Prism.Commands;
using System;
using System.Windows;
using SystemMonitor.Control.Dialogs;
using SystemMonitor.Control.Receipt.ViewModels;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Infrastructure.Interfaces.Printing;

namespace SystemMonitor.Control.Receipt
{
    public class ReceiptViewModel : DialogViewModel
    {
        private readonly IPrinterFacade _printerFacade;

        public ReceiptViewModel(
            IPrinterFacade printerFacade
            )
        {
            _printerFacade = printerFacade;
            PrintCommand = new DelegateCommand<FrameworkElement>(Print);
        }

        public IDeviceInfo Device { get; set; }

        // NOTE: for now it is just dummy data, but should be real data
        // mapped using Automapper from domain class into view model class
        public ReceiptDetailViewModel Receipt => new ReceiptDetailViewModel();

        public DelegateCommand<FrameworkElement> PrintCommand { get; }
        private void Print(FrameworkElement receiptView)
        {
            try
            {
                _printerFacade.Print(receiptView, Device, "Receipt");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, 
                    "Failed to print", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error
                    );
            }
            CloseDialog();
        }
    }
}
