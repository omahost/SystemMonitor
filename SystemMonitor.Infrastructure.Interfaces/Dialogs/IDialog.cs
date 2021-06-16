using Prism.Services.Dialogs;

namespace SystemMonitor.Infrastructure.Interfaces.Dialogs
{
    public interface IDialog : IDialogWindow
    {
        string Title { get; set; }
    }
}