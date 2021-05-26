using System;
using System.Windows;

namespace SystemMonitor.Control.Extensions
{
    public static class ObjectExtensions
    {
        public static void InvokeOnUiThread(this object self, Action callback)
        {
            Application.Current.Dispatcher.Invoke(callback);
        }

        public static T InvokeOnUiThread<T>(this object self, Func<T> action)
        {
            return Application.Current.Dispatcher.Invoke(action);
        }
    }
}
