using Prism.Services.Dialogs;
using System;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Infrastructure.Interfaces.Dialogs
{
    /// <summary>
    /// Extending Prism.Services.Dialogs.IDialogService
    /// </summary>
    public interface IDialogService 
        : Prism.Services.Dialogs.IDialogService
        , ISingletonDependency
    {
        IDialog Create(string name, IDialogParameters parameters, Action<IDialogResult> callback);
    }
}
