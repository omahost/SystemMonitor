using System.Windows;
using SystemMonitor.Control.PinCode.Interfaces;

namespace SystemMonitor.Control.Settings.PinCode
{
    public class SettingsPinCodeFacade 
        : ISettingsPinCodeFacade
    {
        private readonly IPinCodeFacade _pinCodeFacade;

        public SettingsPinCodeFacade(
            IPinCodeFacade pinCodeFacade
            )
        {
            _pinCodeFacade = pinCodeFacade;
        }

        public bool ValidatePinCode()
        {
            var pinCode = _pinCodeFacade.GetPinCode();
            // The PIN for verification will be always the same. Let's say 90021213.
            if (pinCode == "90021213")
            {
                return true;
            }

            // TODO: use custom facade for showing message boxes with proper styling
            MessageBox.Show(
                "Invalid PIN Code", "Error", 
                MessageBoxButton.OK, 
                MessageBoxImage.Exclamation
                );
            return false;
        }
    }
}
