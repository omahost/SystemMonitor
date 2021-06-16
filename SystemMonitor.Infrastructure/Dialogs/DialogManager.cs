using Prism.Services.Dialogs;
using System;
using SystemMonitor.Infrastructure.Interfaces.Dialogs;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Infrastructure.Dialogs
{
    using IDialogService = Interfaces.Dialogs.IDialogService;

    /// <summary>
    /// Wrapper on IDialogService to simplify usage and strong type naming
    /// </summary>
    public class DialogManager : IDialogManager
    {
        private readonly IDialogService _dialogService;
        
        public DialogManager(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public IDialog Create(
            Type dialogType, 
            Action<IDialogResult> callback = null, 
            IDialogParameters parameters = null
            )
        {
            if (callback == null)
            {
                callback = Noop;
            }

            return _dialogService.Create(
                dialogType.GetDependencyName(),
                parameters, callback);
        }

        public void ShowModal(
            Type dialogType, 
            Action<IDialogResult> callback = null, 
            IDialogParameters parameters = null
            )
        {
            if (callback == null)
            {
                callback = Noop;
            }

            _dialogService.ShowDialog(
                dialogType.GetDependencyName(), 
                parameters, callback);
        }
        
        public void Show(
            Type dialogType,
            Action<IDialogResult> callback = null, 
            IDialogParameters parameters = null
            )
        {
            if (callback == null)
            {
                callback = Noop;
            }

            _dialogService.Show(
                dialogType.GetDependencyName(), 
                parameters, callback);
        }

        private static void Noop(IDialogResult result) { }
    }
}
