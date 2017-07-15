using System;
using System.Globalization;
using System.Windows.Data;
using Contracts;

namespace EClient.Converters
{
    public class SelektionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = parameter as string;
            if (value is Selektion && !string.IsNullOrWhiteSpace(param))
            {
                if (param == "A")
                {
                    return (Selektion)value == Selektion.A;
                }

                return (Selektion)value == Selektion.B;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return (string)parameter == "A" ? Selektion.A : Selektion.B;
            }

            return (string)parameter == "A" ? Selektion.B : Selektion.A;
        }
    }
}
