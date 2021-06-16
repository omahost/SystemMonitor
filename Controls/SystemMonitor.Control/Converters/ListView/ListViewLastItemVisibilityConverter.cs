using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SystemMonitor.Control.Converters
{
    [ValueConversion(typeof(ListViewItem), typeof(Visibility))]
    public class ListViewLastItemVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var container = value as ListViewItem;
            if (container == null)
            {
                return Visibility.Collapsed;
            }

            var listView = (ListView)ItemsControl.ItemsControlFromItemContainer(container);

            var ordinal = listView
                .ItemContainerGenerator
                .IndexFromContainer(container)
                + 1;

            if (ordinal == listView.Items.Count)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // This converter does not provide conversion back from ordinal position
            throw new InvalidOperationException();
        }
    }
}
