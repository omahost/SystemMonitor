using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Control.Settings.PinCode
{
    public interface ISettingsPinCodeFacade 
        : ISingletonDependency
    {
        bool ValidatePinCode();
    }
}
