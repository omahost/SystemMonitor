using Prism.Services.Dialogs;
using System;

namespace SystemMonitor.Infrastructure.Interfaces.Dialogs
{
    public interface IDialog : IDialogWindow
    {
        event EventHandler ContentRendered;
        string Title { get; set; }
    }
}