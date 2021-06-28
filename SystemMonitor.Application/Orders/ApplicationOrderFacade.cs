using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using SystemMonitor.Application.Interfaces.Orders;
using SystemMonitor.Application.Interfaces.Orders.Events;
using SystemMonitor.Common.Async;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Application.Orders
{
    public class ApplicationOrderFacade : IApplicationOrderFacade
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplicationOrderApi _applicationOrderApi;
        private readonly List<IApplicationOrder> _orders = new List<IApplicationOrder>();

        public ApplicationOrderFacade(
            IEventAggregator eventAggregator,
            IApplicationOrderApi applicationOrderApi
            )
        {
            _eventAggregator = eventAggregator;
            _applicationOrderApi = applicationOrderApi;

            _eventAggregator
                .GetEvent<ApplicationOrderReceived>()
                .Subscribe(OnApplicationOrderReceived);
        }

        private void OnApplicationOrderReceived(ApplicationOrderReceivedEventArgs args)
        {
            _orders.AddRange(args.Orders);
        }

        public IList<IApplicationOrder> GetOrders()
        {
            return _orders.ToList();
        }

        public void SetOrderAsCanceled(IApplicationOrder order)
        {
            AsyncHelper.RunSync(() =>
                _applicationOrderApi.SetOrderAsCanceledAsync(order.Id)
            );
            order.State = ApplicationTaskStatus.Canceled;
            TriggerOrderStateChanged(order);
        }

        public void SetOrderAsPrinted(IApplicationOrder order)
        {
            AsyncHelper.RunSync(() =>
                _applicationOrderApi.SetOrderAsPrintedAsync(order.Id)
            );
            order.State = ApplicationTaskStatus.Printed;
            TriggerOrderStateChanged(order);
        }

        private void TriggerOrderStateChanged(IApplicationOrder order)
        {
            _eventAggregator
                .GetEvent<ApplicationOrderStateChanged>()
                .Publish(new ApplicationOrderStateChangedEventArgs(order));
        }
    }
}
