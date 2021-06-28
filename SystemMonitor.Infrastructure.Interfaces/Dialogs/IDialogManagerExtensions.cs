using System;

namespace SystemMonitor.Infrastructure.Interfaces.Dialogs
{
    public static class IDialogManagerExtensions
    {
        public static IDialog Create<TDialog>(this IDialogManager self)
        {
            return self.Create(typeof(TDialog));
        }

        public static IDialog CreateDialog<TDialog, TViewModel>(
            this IDialogManager self,
            Action<TViewModel> initialize
            )
        {
            var dialog = self.Create<TDialog>();
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

        public static IDialog ShowDialog<TDialog>(this IDialogManager self)
        {
            var dialog = self.Create<TDialog>();
            dialog.ShowDialog();
            return dialog;
        }
    }
}
