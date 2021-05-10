using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EveOnlineTool
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Do the conversion from bool to visibility

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Do the conversion from visibility to bool

            int result;
            var success = Int32.TryParse(value as string, out result);
            if (success)
                return result;

            return 0;
        }
    }
}
