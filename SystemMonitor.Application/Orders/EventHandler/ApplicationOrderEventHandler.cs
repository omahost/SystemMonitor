using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitor.Application.Interfaces.Devices;
using SystemMonitor.Application.Interfaces.Orders;
using SystemMonitor.Application.Interfaces.Orders.Events;
using SystemMonitor.Application.Interfaces.Tasks.Events;
using SystemMonitor.Control.Receipt.Interfaces;
using SystemMonitor.Control.Receipt.Interfaces.Events;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Application.Orders
{
    public class ApplicationOrderEventHandler : IApplicationOrderEventHandler
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IReceiptPrinterFacade _receiptPrinterFacade;
        private readonly IDeviceMonitorFacade _deviceMonitorFacade;
        private readonly IApplicationOrderFacade _applicationOrderFacade;

        public ApplicationOrderEventHandler(
            IEventAggregator eventAggregator,
            IReceiptPrinterFacade receiptPrinterFacade,
            IDeviceMonitorFacade deviceMonitorFacade,
            IApplicationOrderFacade applicationOrderFacade
            )
        {
            _eventAggregator = eventAggregator;
            _receiptPrinterFacade = receiptPrinterFacade;
            _deviceMonitorFacade = deviceMonitorFacade;
            _applicationOrderFacade = applicationOrderFacade;

            _eventAggregator
                .GetEvent<ApplicationOrderReceived>()
                .Subscribe(OnApplicationOrderReceived);

            _eventAggregator
               .GetEvent<ApplicationOrderPrinted>()
               .Subscribe(OnApplicationOrderPrinted);

            _eventAggregator
                .GetEvent<ApplicationTaskDeviceChanged>()
                .Subscribe(OnApplicationTaskDeviceChanged);
        }

        private void OnApplicationOrderReceived(ApplicationOrderReceivedEventArgs args)
        {
            OnApplicationOrderReceived(args.Orders);
        }

        private void OnApplicationOrderReceived(IReadOnlyList<IApplicationOrder> orders)
        {
            _ = PrintOrdersAsync(orders);
        }

        private async Task PrintOrdersAsync(IReadOnlyList<IApplicationOrder> orders)
        {
            foreach (var order in orders)
            {
                try
                {
                    await PrintOrderAsync(order);
                }
                catch (Exception ex)
                {
                    // TODO: log/handle
                }
            }
        }

        private async Task PrintOrderAsync(IApplicationOrder order)
        {
            var device = _deviceMonitorFacade.FindDeviceById(order.Task.DeviceId);
            if (device == null)
            {
                return;
            }

            await _receiptPrinterFacade.PrintAsync(order, device);
        }

        private void OnApplicationOrderPrinted(ApplicationOrderPrintedEventArgs args)
        {
            _applicationOrderFacade.SetOrderAsPrinted(args.Order);
        }

        private void OnApplicationTaskDeviceChanged(ApplicationTaskDeviceChangedEventArgs args)
        {
            var task = args.Task;
            if (args.HasPreviousDeviceId)
            {
                return;
            }

            var ordersToPrint = _applicationOrderFacade.GetOrders()
                .Where(order => order.Task == args.Task)
                .Where(order => order.State == ApplicationTaskStatus.Waiting)
                .ToList();

            _ = PrintOrdersAsync(ordersToPrint);
        }
    }
}
