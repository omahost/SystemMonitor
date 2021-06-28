using System.Collections.Generic;

namespace SystemMonitor.Control.Receipt.ViewModels
{
    public class OrderItemsViewModels : Views.ViewModelBase
    {
        private readonly List<OrderItemViewModel> _items;
        public OrderItemsViewModels(params OrderItemViewModel[] items)
        {
            _items = new List<OrderItemViewModel>(items);
        }

        public IReadOnlyCollection<OrderItemViewModel> Items 
            => _items.AsReadOnly();

        public void AddItems(IEnumerable<OrderItemViewModel> items)
        {
            _items.AddRange(items);
        }
    }
}
