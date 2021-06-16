using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using SystemMonitor.Control.Extensions;
using SystemMonitor.Control.Views;

namespace SystemMonitor.Control.Dialogs
{
    public class DialogViewModel 
        : ViewModelBase
        , IDialogAware
    {
        protected IDialogParameters _parameters;
        public virtual string Title { get; set; } = string.Empty;

        public event Action<IDialogResult> RequestClose;

        private DelegateCommand<string> _closeCommand;
        public DelegateCommand<string> CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new DelegateCommand<string>(CloseDialog);
                }
                return _closeCommand;
            }
        }

        public virtual void CloseDialog(string parameter)
        {
            if (!Enum.TryParse<ButtonResult>(parameter, true, out var result))
            {
                result = ButtonResult.None;
            }
            CloseDialog(result);
        }

        public virtual void CloseDialog(ButtonResult buttonResult)
        {
            CloseDialog(new DialogResult(buttonResult, _parameters));
        }

        public virtual void CloseDialog()
        {
            CloseDialog(ButtonResult.Cancel);
        }

        public virtual void CloseDialog(DialogResult dialogResult)
        {
            this.InvokeOnUiThread(() =>
            {
                RequestClose?.Invoke(dialogResult);
            });
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            _parameters = parameters ?? new DialogParameters();
            AddDialogParameter(this);
            
            var title = _parameters.GetValue<string>(nameof(Title));
            if (!string.IsNullOrWhiteSpace(title))
            {
                Title = title;
            }
        }

        protected void AddDialogParameter<T>(T value)
        {
            var type = typeof(T);
            var key = type.Name;
            if (!_parameters.ContainsKey(key))
            {
                _parameters.Add(key, value);
            }
        }
    }
}
