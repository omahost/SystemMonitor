using System.Printing;
using System.Windows.Controls;
using System.Windows.Media;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Infrastructure.Interfaces.Printing;

namespace SystemMonitor.Infrastructure.Printing
{
    public class PrinterFacade : IPrinterFacade
    {
        public void Print(Visual visual, IDeviceInfo device, string description)
        {
            var printDialog = new PrintDialog
            {
                PrintQueue = new PrintQueue(new PrintServer(), device.Name)
            };
            printDialog.PrintVisual(visual, description);
        }
    }
}
