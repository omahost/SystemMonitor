using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Control.PinCode.Interfaces
{
    public interface IPinCodeFacade
        : ISingletonDependency
    {
        string GetPinCode();
    }
}
