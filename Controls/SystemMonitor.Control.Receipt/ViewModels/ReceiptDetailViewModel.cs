using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SystemMonitor.Control.Receipt.ViewModels
{
    // NOTE: using inline dummy data, in real project should 
    // be used Automapper from domain object into view model
    public class ReceiptDetailViewModel : Views.ViewModelBase
    {
        private DateTime _receiptDate = new DateTime(2021, 05, 12, 12, 20, 53);
        public DateTime ReceiptDate
        {
            get => _receiptDate;
            set => SetProperty(ref _receiptDate, value);
        }

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

        private string _ticketNumber = "282";
        public string TicketNumber
        {
            get => _ticketNumber;
            set => SetProperty(ref _ticketNumber, value);
        }

        private string _waiterName = "Imie kelnera";
        public string WaiterName
        {
            get => _waiterName;
            set => SetProperty(ref _waiterName, value);
        }

        private string _tableNumber = "5A";
        public string TableNumber
        {
            get => _tableNumber;
            set => SetProperty(ref _tableNumber, value);
        }

        private string _serviceName = "Kuchnia";
        public string ServiceName
        {
            get => _serviceName;
            set => SetProperty(ref _serviceName, value);
        }

        private uint _guestsCount = 2;
        public uint GuestsCount
        {
            get => _guestsCount;
            set => SetProperty(ref _guestsCount, value);
        }

        private string _serviceTypeName = "pizze zapakować";
        public string ServiceTypeName
        {
            get => _serviceTypeName;
            set => SetProperty(ref _serviceTypeName, value);
        }

        public string ServiceTypeDescription =>
            $"{ServiceTypeName}, dostawa {DeliveryTime.Hours}:{DeliveryTime.Minutes}";

        private TimeSpan _deliveryTime = new TimeSpan(12, 40, 0);
        public TimeSpan DeliveryTime
        {
            get => _deliveryTime;
            set => SetProperty(ref _deliveryTime, value);
        }

        private string _orderDescription = "w lokalu (lub na wynos)";
        public string OrderDescription
        {
            get => _orderDescription;
            set => SetProperty(ref _orderDescription, value);
        }

        private List<OrderItemsViewModels> _orderHistory = new List<OrderItemsViewModels>
        {
            new OrderItemsViewModels
            (
                new OrderItemViewModel
                {
                    Title = "ROSÓŁ",
                    Description = "bez zielonego",
                    Quantity = 1
                },
                new OrderItemViewModel
                {
                    Title = "ZUPQ KREM Z CUKINII",
                    Quantity = 2
                }
            ),

            new OrderItemsViewModels
            (
                new OrderItemViewModel
                {
                    Title = "SCHABOWY",
                    Description = "dla dziecka bez panierki",
                    Quantity = 2
                },
                new OrderItemViewModel
                {
                    Title = "PIZZA MARGARITKA +SER +PIECZARKI",
                    Description = "(klient 1)",
                    Quantity = 1
                }
            )
        };

        public ReadOnlyCollection<OrderItemsViewModels> OrderHistory 
            => _orderHistory.AsReadOnly();
    }
}
