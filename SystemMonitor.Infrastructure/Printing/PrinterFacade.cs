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
            var printDialog = CreatePrintDialog(device);
            printDialog.PrintVisual(visual, description);
        }

        private PrintDialog CreatePrintDialog(IDeviceInfo device)
        {
            return new PrintDialog
            {
                PrintQueue = CreatePrintQueue(device)
            };
        }

        private PrintQueue CreatePrintQueue(IDeviceInfo device)
        {
            var printServer = CreatePrintServer(device);
            return new PrintQueue(printServer, device.Name);
        }

        private PrintServer CreatePrintServer(IDeviceInfo device)
        {
            if (device.IsRemote)
            {
                return new PrintServer(device.ServerPath);
            }
            return new PrintServer();
        }
    }
}
