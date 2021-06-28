using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using SystemMonitor.Control.Dialogs;
using SystemMonitor.Control.Receipt.Interfaces.Events;
using SystemMonitor.Control.Receipt.ViewModels;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Infrastructure.Interfaces.Printing;

namespace SystemMonitor.Control.Receipt
{
    public class ReceiptViewModel : DialogViewModel
    {
        private readonly IPrinterFacade _printerFacade;
        private readonly IEventAggregator _eventAggregator;

        public ReceiptViewModel(
            IPrinterFacade printerFacade,
            IEventAggregator eventAggregator
            )
        {
            _printerFacade = printerFacade;
            _eventAggregator = eventAggregator;

            PrintCommand = new DelegateCommand(Print);
        }

        public IDeviceInfo Device { get; set; }

        private IApplicationOrder _order;
        public IApplicationOrder Order 
        {
            get => _order;
            set
            {
                if (_order == value)
                {
                    return;
                }

                _order = value;
                if (_order != null)
                {
                    Receipt = new ReceiptDetailViewModel(value);
                }
                else
                {
                    Receipt = null;
                }
            }
        }

        private ReceiptDetailViewModel _receipt;
        public ReceiptDetailViewModel Receipt
        {
            get => _receipt;
            set => SetProperty(ref _receipt, value);
        }

        private FrameworkElement _receiptView;
        public FrameworkElement ReceiptView
        {
            get => _receiptView;
            set => SetProperty(ref _receiptView, value);
        }

        public DelegateCommand PrintCommand { get; }
        public void Print()
        {
            try
            {
                _printerFacade.Print(ReceiptView, Device, "Receipt");
                TriggerPrintedEvent();
                CloseDialog(ButtonResult.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Failed to print",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                CloseDialog(ButtonResult.Cancel);
            }
        }

        private void TriggerPrintedEvent()
        {
            _eventAggregator
                .GetEvent<ApplicationOrderPrinted>()
                .Publish(new ApplicationOrderPrintedEventArgs(Order));
        }
    }
}
