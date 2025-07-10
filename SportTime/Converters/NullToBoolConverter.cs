using System.Globalization;

namespace SportTime.Converters
{
    /// <summary>
    /// Null qiymatni bool ga o'tkazish uchun converter
    /// Null uchun false, null bo'lmagan uchun true qaytaradi
    /// </summary>
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}