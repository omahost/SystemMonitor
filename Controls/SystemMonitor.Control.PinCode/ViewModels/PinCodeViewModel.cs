using Prism.Commands;
using Prism.Services.Dialogs;
using SystemMonitor.Control.Dialogs;

namespace SystemMonitor.Control.PinCode
{
    public class PinCodeViewModel 
        : DialogViewModel
        , IPinCodeViewModel
    {
        public PinCodeViewModel()
        {
            CancelCommand = new DelegateCommand(Cancel);
            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            ResetPinCode();
            base.OnDialogOpened(parameters);
        }

        private string _pinCode;
        public string PinCode
        {
            get => _pinCode;
            set
            {
                if (SetProperty(ref _pinCode, value?.Trim()))
                {
                    SubmitCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand CancelCommand { get; }
        private void Cancel()
        {
            ResetPinCode();
            CloseDialog(ButtonResult.Cancel);
        }

        public DelegateCommand SubmitCommand { get; }
        private void Submit()
        {
            CloseDialog(ButtonResult.OK);
        }

        private bool CanSubmit()
        {
            return !string.IsNullOrWhiteSpace(PinCode);
        }

        private void ResetPinCode()
        {
            PinCode = string.Empty;
        }
    }
}
