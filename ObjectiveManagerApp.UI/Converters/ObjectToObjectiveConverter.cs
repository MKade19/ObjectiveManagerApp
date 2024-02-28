using ObjectiveManagerApp.Common.Models;
using System.Globalization;
using System.Windows.Data;

namespace ObjectiveManagerApp.UI.Converters
{
    public class ObjectToObjectiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Objective)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
