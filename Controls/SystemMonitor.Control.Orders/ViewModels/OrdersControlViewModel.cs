using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitor.Application.Interfaces.Orders;
using SystemMonitor.Application.Interfaces.Orders.Events;
using SystemMonitor.Common.Extensions;
using SystemMonitor.Control.Dialogs;
using SystemMonitor.Control.Extensions;
using SystemMonitor.Domain.Interfaces.Orders;

namespace SystemMonitor.Control.Orders
{
    public class OrdersControlViewModel : DialogViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplicationOrderFacade _applicationOrderFacade;

        public OrdersControlViewModel(
            IEventAggregator eventAggregator,
            IApplicationOrderFacade applicationOrderFacade
            )
        {
            _eventAggregator = eventAggregator;
            _applicationOrderFacade = applicationOrderFacade;

            _eventAggregator
               .GetEvent<ApplicationOrderStateChanged>()
               .Subscribe(OnApplicationOrderStateChanged);

            _eventAggregator
                .GetEvent<ApplicationOrderReceived>()
                .Subscribe(OnApplicationOrderReceived);

            InitializeAsync();
        }

        private void OnApplicationOrderReceived(ApplicationOrderReceivedEventArgs args)
        {
            this.InvokeOnUiThread(() => OnApplicationOrderReceived(args.Orders));
        }

        private void OnApplicationOrderReceived(IReadOnlyList<IApplicationOrder> orders)
        {
            Orders?.AddRange(orders.Select(CreateOrderViewModel));
        }

        private void InitializeAsync()
        {
            Task.Run(Initialize);
        }

        private void Initialize()
        {
            Orders?.Clear();

            var orders = _applicationOrderFacade
                .GetOrders()
                .Select(CreateOrderViewModel)
                .ToObservableCollection();

            this.InvokeOnUiThread(() => Orders = orders);
        }

        private OrderViewModel CreateOrderViewModel(IApplicationOrder order)
        {
            return new OrderViewModel(_applicationOrderFacade, order);
        }

        private ObservableCollection<OrderViewModel> _orders;
        public ObservableCollection<OrderViewModel> Orders
        {
            get => _orders;
            private set => SetProperty(ref _orders, value);
        }

        private void OnApplicationOrderStateChanged(ApplicationOrderStateChangedEventArgs args)
        {
            var viewModel = Orders.FirstOrDefault(
                orderViewModel => orderViewModel.Order == args.Order);

            if (viewModel != null)
            {
                viewModel.UpdateState();
            }
        }
    }
}
