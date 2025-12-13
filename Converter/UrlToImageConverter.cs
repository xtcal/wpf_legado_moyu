using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
namespace wpf_legado_moyu
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class UrlToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var url = (string)value;

            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri(url, UriKind.Absolute);
            //bitmap.EndInit();
            //return bitmap;

            return new BitmapImage(new Uri(url, UriKind.Absolute));
        }
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}