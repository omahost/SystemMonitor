using System.Windows.Controls;
using SystemMonitor.Control.Receipt.Interfaces;

namespace SystemMonitor.Control.Receipt
{
    public partial class ReceiptView 
        : UserControl
        , IReceiptView
    {
        public ReceiptView()
        {
            InitializeComponent();
        }
    }
}
