using Prism.Commands;
using Prism.Mvvm;
using System;
using SystemMonitor.Application.Interfaces.Orders;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Control.Orders
{
    public class OrderViewModel : BindableBase
    {
        private readonly IApplicationOrder _applicationOrder;
        private readonly IApplicationOrderFacade _applicationOrderFacade;

        public OrderViewModel(
            IApplicationOrderFacade applicationOrderFacade,
            IApplicationOrder applicationOrder
            )
        {
            _applicationOrder = applicationOrder;
            _applicationOrderFacade = applicationOrderFacade;

            CancelPrintCommand = new DelegateCommand(CancelPrint, CanCancelPrint);
        }

        public IApplicationOrder Order => _applicationOrder;
        public int Id => _applicationOrder.Id;
        public string OrderNote => _applicationOrder.OrderNote;
        public string Waiter => _applicationOrder.Waiter;
        public string Category => _applicationOrder.Task.Name;
        public string TableNumber => _applicationOrder.TableNumber;
        public int CustomersCount => _applicationOrder.CustomersCount;
        public DateTime OrderedAt => _applicationOrder.OrderedAt;
        public ApplicationTaskStatus State => _applicationOrder.State;
        public int ItemsCount => _applicationOrder.Items.Count;
        public string OrderType
        {
            get
            {
                var orderType = _applicationOrder.OrderType;
                if (orderType == ApplicationOrderType.InsideAndOutside)
                {
                    return "Inside | Outside";
                }
                return orderType.ToString();
            }
        }

        public DelegateCommand CancelPrintCommand { get; }
        private void CancelPrint()
        {
            _applicationOrderFacade.SetOrderAsCanceled(_applicationOrder);
            UpdateState();
        }

        public void UpdateState()
        {
            RaisePropertyChanged(nameof(State));
            CancelPrintCommand.RaiseCanExecuteChanged();
        }

        private bool CanCancelPrint()
        {
            return State == ApplicationTaskStatus.Waiting;
        }
    }
}
