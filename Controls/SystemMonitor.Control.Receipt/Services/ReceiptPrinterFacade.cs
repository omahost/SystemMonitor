using System.Threading.Tasks;
using SystemMonitor.Control.Extensions;
using SystemMonitor.Control.Receipt.Interfaces;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Infrastructure.Interfaces.Dialogs;

namespace SystemMonitor.Control.Receipt.Services
{
    public class ReceiptPrinterFacade : IReceiptPrinterFacade
    {
        private readonly IDialogManager _dialogManager;

        public ReceiptPrinterFacade(
            IDialogManager dialogManager
            )
        {
            _dialogManager = dialogManager;
        }

        public Task PrintAsync(IApplicationOrder order, IDeviceInfo deviceInfo)
        {
            return this.InvokeOnUiThread(() => _PrintAsync(order, deviceInfo));
        }

        public Task _PrintAsync(IApplicationOrder order, IDeviceInfo deviceInfo)
        {
            var taskSource = new TaskCompletionSource<bool>();
            Task.Delay(5000).ContinueWith(t => taskSource.TrySetCanceled());

            var task = taskSource.Task;
            ReceiptViewModel viewModel = null;
            var dialog = _dialogManager.CreateDialog<IReceiptView, ReceiptViewModel>((vm) =>
            {
                viewModel = vm;
                viewModel.Order = order;
                viewModel.Device = deviceInfo;
            });

            dialog.ContentRendered += (sender, e) =>
            {
                if (task.IsCanceled || task.IsCompleted)
                {
                    return;
                }

                viewModel.Print();
                taskSource.TrySetResult(true);
            };

            dialog.Show();
            return task;
        }
    }
}
