using System.Windows.Controls;
using SystemMonitor.Control.Orders.Interfaces;

namespace SystemMonitor.Control.Orders
{
    public partial class OrdersControl 
        : UserControl
        , IOrdersView
    {
        public OrdersControl()
        {
            InitializeComponent();
        }
    }
}
