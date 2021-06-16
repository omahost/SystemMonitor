using System;

namespace SystemMonitor.Infrastructure.Interfaces.Dialogs
{
    public static class IDialogManagerExtensions
    {
        public static IDialog CreateDialog<TDialog, TViewModel>(
            this IDialogManager self,
            Action<TViewModel> initialize
            )
        {
            var dialog = self.Create(typeof(TDialog));
            var dialogAware = dialog.GetDialogViewModel();
            if (dialogAware is TViewModel viewModel)
            {
                initialize(viewModel);
            }
            else
            {
                throw new InvalidCastException(
                    $"{dialogAware.GetType().Name} do not implement {typeof(TViewModel).Name}");
            }
            return dialog;
        }
    }
}
