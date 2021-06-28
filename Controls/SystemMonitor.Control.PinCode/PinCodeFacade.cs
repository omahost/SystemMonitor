using SystemMonitor.Control.PinCode.Interfaces;
using SystemMonitor.Infrastructure.Interfaces.Dialogs;

namespace SystemMonitor.Control.PinCode
{
    public class PinCodeFacade : IPinCodeFacade
    {
        private readonly IDialogManager _dialogManager;

        public PinCodeFacade(
            IDialogManager dialogManager
            )
        {
            _dialogManager = dialogManager;
        }

        public string GetPinCode()
        {
            var dialog = _dialogManager.Create<IPinCodeView>();
            var viewModel = (IPinCodeViewModel)dialog.GetDialogViewModel();
            dialog.ShowDialog();
            return viewModel.PinCode;
        }
    }
}
