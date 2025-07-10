using System.Collections.ObjectModel;
using System.Globalization;
using SportTime.Models;

namespace SportTime.Converters
{
    /// <summary>
    /// Bajarilgan vazifalar sonini hisoblash uchun converter
    /// </summary>
    public class CompletedTasksCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<TaskModel> tasks)
            {
                return tasks.Count(t => t.IsCompleted);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}