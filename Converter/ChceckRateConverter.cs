using System.Globalization;
using System.Windows.Data;
namespace wpf_legado_moyu
{
    [ValueConversion(typeof(int), typeof(string))]
    public class ChceckRateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rate = (int)value;
            return rate.ToString();
        }
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var url = (string)value;
            int.TryParse(url, out var rateValue);
            rateValue = Math.Clamp(rateValue, -10, 10);
            return rateValue;
        }
    }
}