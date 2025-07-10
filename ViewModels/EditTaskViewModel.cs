using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.ViewModels
{
    [QueryProperty(nameof(TaskToEdit), "TaskToEdit")] // Navigatsiya orqali parametrni qabul qilish
    public partial class EditTaskViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly TaskViewModel _mainViewModel; // Asosiy ViewModelga o'zgarishlarni xabardor qilish uchun

        [ObservableProperty]
        TaskModel _taskToEdit;

        public EditTaskViewModel(DatabaseService databaseService, TaskViewModel mainViewModel)
        {
            _databaseService = databaseService;
            _mainViewModel = mainViewModel; // Asosiy ViewModelni inject qilish
        }

        [RelayCommand]
        private async Task SaveTaskAsync()
        {
            if (TaskToEdit == null) return;

            if (string.IsNullOrWhiteSpace(TaskToEdit.Title))
            {
                await Shell.Current.DisplayAlert("Xatolik", "Vazifa sarlavhasi bo'sh bo'lishi mumkin emas.", "OK");
                return;
            }

            await _databaseService.SaveTaskAsync(TaskToEdit);

            // Asosiy ro'yxatni yangilash
            if (_mainViewModel.LoadTasksCommand.CanExecute(null))
            {
                await _mainViewModel.LoadTasksCommand.ExecuteAsync(null);
            }

            await Shell.Current.GoToAsync(".."); // Orqaga qaytish
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync(".."); // Orqaga qaytish
        }

        // TaskToEdit xususiyati o'zgarganda (navigatsiyadan keyin) kerak bo'lishi mumkin
        // partial void OnTaskToEditChanged(TaskModel value)
        // {
        //     // Agar nusxa bilan ishlamoqchi bo'lsangiz, bu yerda nusxa yaratishingiz mumkin
        //     // Hozircha to'g'ridan-to'g'ri obyekt bilan ishlaymiz
        // }
    }
}
