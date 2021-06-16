﻿using Prism.Common;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using SystemMonitor.Infrastructure.Interfaces.Dialogs;

namespace SystemMonitor.Infrastructure.Dialogs
{
    /// <summary>
    /// Implements <see cref="DevExpress.Mvvm.IDialogService"/> to show modal and non-modal dialogs.
    /// https://github.com/PrismLibrary/Prism/blob/master/src/Wpf/Prism.Wpf/Services/Dialogs/DialogService.cs
    /// </summary>
    /// <remarks>
    /// The dialog's ViewModel must implement IDialogAware.
    /// </remarks>
    public class DialogService : Interfaces.Dialogs.IDialogService
    {
        private readonly IContainerExtension _containerExtension;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogService"/> class.
        /// </summary>
        /// <param name="containerExtension"></param>
        public DialogService(IContainerExtension containerExtension)
        {
            _containerExtension = containerExtension;
        }

        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        public void Show(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            ShowDialogInternal(name, parameters, callback, false);
        }

        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <param name="windowName">The name of the hosting window registered with the IContainerRegistry.</param>
        public void Show(string name, IDialogParameters parameters, Action<IDialogResult> callback, string windowName)
        {
            ShowDialogInternal(name, parameters, callback, false, windowName);
        }

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        public void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            ShowDialogInternal(name, parameters, callback, true);
        }

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <param name="windowName">The name of the hosting window registered with the IContainerRegistry.</param>
        public void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback, string windowName)
        {
            ShowDialogInternal(name, parameters, callback, true, windowName);
        }

        void ShowDialogInternal(string name, IDialogParameters parameters, Action<IDialogResult> callback, bool isModal, string windowName = null)
        {
            IDialogWindow dialogWindow = CreateDialogWindow(windowName);
            ConfigureDialogWindowEvents(dialogWindow, callback);
            ConfigureDialogWindowContent(name, dialogWindow, parameters);

            if (isModal)
                dialogWindow.ShowDialog();
            else
                dialogWindow.Show();
        }

        /// <summary>
        /// Create a new <see cref="IDialogWindow"/>.
        /// </summary>
        /// <param name="name">The name of the hosting window registered with the IContainerRegistry.</param>
        /// <returns>The created <see cref="IDialogWindow"/>.</returns>
        protected virtual IDialogWindow CreateDialogWindow(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return _containerExtension.Resolve<IDialogWindow>();
            else
                return _containerExtension.Resolve<IDialogWindow>(name);
        }

        /// <summary>
        /// Configure <see cref="IDialogWindow"/> content.
        /// </summary>
        /// <param name="dialogName">The name of the dialog to show.</param>
        /// <param name="window">The hosting window.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        protected virtual void ConfigureDialogWindowContent(string dialogName, IDialogWindow window, IDialogParameters parameters)
        {
            var content = _containerExtension.Resolve<object>(dialogName);
            var dialogContent = content as FrameworkElement;
            if (dialogContent == null)
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            var viewModel = dialogContent.DataContext as IDialogAware;
            if (viewModel == null)
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            ConfigureDialogWindowProperties(window, dialogContent, viewModel);

            MvvmHelpers.ViewAndViewModelAction<IDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
        }

        /// <summary>
        /// Configure <see cref="IDialogWindow"/> and <see cref="IDialogAware"/> events.
        /// </summary>
        /// <param name="dialogWindow">The hosting window.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        protected virtual void ConfigureDialogWindowEvents(IDialogWindow dialogWindow, Action<IDialogResult> callback)
        {
            Action<IDialogResult> requestCloseHandler = null;
            requestCloseHandler = (o) =>
            {
                dialogWindow.Result = o;
                dialogWindow.Close();
            };

            RoutedEventHandler loadedHandler = null;
            loadedHandler = (o, e) =>
            {
                dialogWindow.Loaded -= loadedHandler;
                dialogWindow.GetDialogViewModel().RequestClose += requestCloseHandler;
            };
            dialogWindow.Loaded += loadedHandler;

            CancelEventHandler closingHandler = null;
            closingHandler = (o, e) =>
            {
                if (!dialogWindow.GetDialogViewModel().CanCloseDialog())
                    e.Cancel = true;
            };
            dialogWindow.Closing += closingHandler;

            EventHandler closedHandler = null;
            closedHandler = (o, e) =>
            {
                dialogWindow.Closed -= closedHandler;
                dialogWindow.Closing -= closingHandler;
                dialogWindow.GetDialogViewModel().RequestClose -= requestCloseHandler;

                dialogWindow.GetDialogViewModel().OnDialogClosed();

                if (dialogWindow.Result == null)
                    dialogWindow.Result = new DialogResult();

                callback?.Invoke(dialogWindow.Result);

                dialogWindow.DataContext = null;
                dialogWindow.Content = null;
            };
            dialogWindow.Closed += closedHandler;
        }

        /// <summary>
        /// Configure <see cref="IDialogWindow"/> properties.
        /// </summary>
        /// <param name="window">The hosting window.</param>
        /// <param name="dialogContent">The dialog to show.</param>
        /// <param name="viewModel">The dialog's ViewModel.</param>
        protected virtual void ConfigureDialogWindowProperties(IDialogWindow window, FrameworkElement dialogContent, IDialogAware viewModel)
        {
            var windowStyle = Dialog.GetWindowStyle(dialogContent);
            if (windowStyle != null)
                window.Style = windowStyle;

            window.Content = dialogContent;
            window.DataContext = viewModel; //we want the host window and the dialog to share the same data context

            if (window.Owner == null)
                window.Owner = Application.Current?.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
        }

        public IDialog Create(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            IDialogWindow dialogWindow = CreateDialogWindow(null);
            ConfigureDialogWindowEvents(dialogWindow, callback);
            ConfigureDialogWindowContent(name, dialogWindow, parameters);
            return new DialogWindow(dialogWindow);
        }
    }
}