using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace SystemMonitor.Control.Converters
{
    [ValueConversion(typeof(ListViewItem), typeof(int))]
    public class ListViewOrdinalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var container = value as ListViewItem;
            if (container == null)
            {
                return -1;
            }

            var listView = (ListView)ItemsControl.ItemsControlFromItemContainer(container);

            var ordinal = listView
                .ItemContainerGenerator
                .IndexFromContainer(container)
                + 1;

            return ordinal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // This converter does not provide conversion back from ordinal position
            throw new InvalidOperationException();
        }
    }
}
