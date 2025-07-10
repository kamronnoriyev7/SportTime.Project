using System.Globalization;

namespace SportTime.Converters
{
    /// <summary>
    /// String qiymatni bool ga o'tkazish uchun converter
    /// Bo'sh string uchun false, to'ldirilgan uchun true qaytaradi
    /// </summary>
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value?.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}