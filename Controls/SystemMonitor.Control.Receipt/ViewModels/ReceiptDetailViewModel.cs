using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SystemMonitor.Domain.Interfaces.Orders;

namespace SystemMonitor.Control.Receipt.ViewModels
{
    // NOTE: using inline dummy data, in real project should 
    // be used Automapper from domain object into view model
    public class ReceiptDetailViewModel : Views.ViewModelBase
    {
        private readonly IApplicationOrder _applicationOrder;

        public ReceiptDetailViewModel(
            IApplicationOrder applicationOrder
            )
        {
            _applicationOrder = applicationOrder;
            InitializeOrderItems();
        }

        public DateTime ReceiptDate => _applicationOrder.OrderedAt;

        private string _businessName = "gorny margines";
        public string BusinessName
        {
            get => _businessName;
            set => SetProperty(ref _businessName, value);
        }

        private string _businessTitle = "do powieszenia";
        public string BusinessTitle
        {
            get => _businessTitle;
            set => SetProperty(ref _businessTitle, value);
        }

        public string TicketNumber => _applicationOrder.Id.ToString();
        public string WaiterName => _applicationOrder.Waiter;
        public string TableNumber => _applicationOrder.TableNumber;
        public string ServiceName => _applicationOrder.Task.Name;
        public int GuestsCount => _applicationOrder.CustomersCount;
        public string ServiceTypeDescription => _applicationOrder.OrderNote;

        public string OrderDescription
        {
            get 
            {
                var orderType = _applicationOrder.OrderType;
                if (orderType == ApplicationOrderType.Inside)
                {
                    return "Inside";
                }

                if (orderType == ApplicationOrderType.Outside)
                {
                    return "Outside";
                }

                if (orderType == ApplicationOrderType.InsideAndOutside)
                {
                    return "Inside | Outside";
                }

                return string.Empty;
            }
        }

        private List<OrderItemsViewModels> _orderHistory;
        private void InitializeOrderItems()
        {
            _orderHistory = new List<OrderItemsViewModels>();
            var groupedItems = _applicationOrder.Items.GroupBy(item => item.Rank);
            foreach (var group in groupedItems)
            {
                var orderItemsViewModels = new OrderItemsViewModels();
                _orderHistory.Add(orderItemsViewModels);

                var orderItemViewModels = group.Select(item => new OrderItemViewModel
                {
                    Title = item.Name,
                    Description = item.Note,
                    Quantity = item.Quantity
                });

                orderItemsViewModels.AddItems(orderItemViewModels);
            }
        }
        
        public ReadOnlyCollection<OrderItemsViewModels> OrderHistory 
            => _orderHistory.AsReadOnly();
    }
}
