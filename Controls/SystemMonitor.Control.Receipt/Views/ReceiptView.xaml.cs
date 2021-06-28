using System.Windows.Controls;
using SystemMonitor.Control.Receipt.Interfaces;

namespace SystemMonitor.Control.Receipt
{
    public partial class ReceiptView 
        : UserControl
        , IReceiptView
    {
        private ReceiptViewModel ViewModel 
            => (ReceiptViewModel)DataContext;

        public ReceiptView()
        {
            InitializeComponent();
            ViewModel.ReceiptView = receiptView;
        }
    }
}
