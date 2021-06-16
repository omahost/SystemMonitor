using SystemMonitor.Interfaces.Ioc;
using Prism.Services.Dialogs;
using System;

namespace SystemMonitor.Infrastructure.Interfaces.Dialogs
{
    /// <summary>
    /// Wrapper for <see cref="IDialogService"/> to show modal and non-modal dialogs.
    /// Instead of string names of dialogs are used strong types
    /// </summary>
    public interface IDialogManager : ISingletonDependency
    {
        IDialog Create(Type dialogType, Action<IDialogResult> callback = null, IDialogParameters parameters = null);
        void ShowModal(Type dialogType, Action<IDialogResult> callback = null, IDialogParameters parameters = null);
        void Show(Type dialogType, Action<IDialogResult> callback = null, IDialogParameters parameters = null);
    }
}

