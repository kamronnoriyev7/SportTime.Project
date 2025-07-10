using System.Collections.ObjectModel;
using System.Windows.Input;
using SportTime.Models;
using SportTime.Services;
using SportTime.Views;

namespace SportTime.ViewModels
{
    /// <summary>
    /// Asosiy sahifa uchun ViewModel
    /// Vazifalar ro'yxatini boshqarish va CRUD operatsiyalari
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<TaskModel> Tasks { get; set; }
        public ObservableCollection<TaskModel> FilteredTasks { get; set; }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterTasks();
            }
        }

        private bool _showCompletedOnly;
        public bool ShowCompletedOnly
        {
            get => _showCompletedOnly;
            set
            {
                SetProperty(ref _showCompletedOnly, value);
                FilterTasks();
            }
        }

        // Commands - UI elementlari bilan bog'lanish uchun
        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ToggleCompletionCommand { get; }
        public ICommand RefreshCommand { get; }

        public MainViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Tasks = new ObservableCollection<TaskModel>();
            FilteredTasks = new ObservableCollection<TaskModel>();

            // Command'larni bog'lash
            AddTaskCommand = new Command(async () => await AddTaskAsync());
            EditTaskCommand = new Command<TaskModel>(async (task) => await EditTaskAsync(task));
            DeleteTaskCommand = new Command<TaskModel>(async (task) => await DeleteTaskAsync(task));
            ToggleCompletionCommand = new Command<TaskModel>(async (task) => await ToggleCompletionAsync(task));
            RefreshCommand = new Command(async () => await LoadTasksAsync());
        }

        /// <summary>
        /// Vazifalarni ma'lumotlar bazasidan yuklash
        /// </summary>
        public async Task LoadTasksAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var tasks = await _databaseService.GetAllTasksAsync();
                
                Tasks.Clear();
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
                
                FilterTasks();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Xatolik", 
                    $"Vazifalarni yuklashda xatolik: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Vazifalarni qidirish va filtrlash
        /// </summary>
        private void FilterTasks()
        {
            FilteredTasks.Clear();
            
            var filteredList = Tasks.AsEnumerable();

            // Qidirish bo'yicha filtrlash
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredList = filteredList.Where(t => 
                    t.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            // Bajarilgan vazifalar bo'yicha filtrlash
            if (ShowCompletedOnly)
            {
                filteredList = filteredList.Where(t => t.IsCompleted);
            }

            foreach (var task in filteredList)
            {
                FilteredTasks.Add(task);
            }
        }

        /// <summary>
        /// Yangi vazifa qo'shish sahifasiga o'tish
        /// </summary>
        private async Task AddTaskAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddEditTaskPage));
        }

        /// <summary>
        /// Vazifani tahrirlash sahifasiga o'tish
        /// </summary>
        private async Task EditTaskAsync(TaskModel task)
        {
            if (task == null) return;
            
            var navigationParameter = new Dictionary<string, object>
            {
                { "Task", task }
            };
            
            await Shell.Current.GoToAsync(nameof(AddEditTaskPage), navigationParameter);
        }

        /// <summary>
        /// Vazifani o'chirish (tasdiqlash bilan)
        /// </summary>
        private async Task DeleteTaskAsync(TaskModel task)
        {
            if (task == null) return;

            var result = await Application.Current.MainPage.DisplayAlert(
                "Tasdiqlash", 
                $"'{task.Title}' vazifasini o'chirishni xohlaysizmi?", 
                "Ha", "Yo'q");

            if (result)
            {
                try
                {
                    await _databaseService.DeleteTaskAsync(task.Id);
                    Tasks.Remove(task);
                    FilterTasks();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Xatolik", 
                        $"Vazifani o'chirishda xatolik: {ex.Message}", "OK");
                }
            }
        }

        /// <summary>
        /// Vazifani bajarildi/bajarilmadi holatini o'zgartirish
        /// </summary>
        private async Task ToggleCompletionAsync(TaskModel task)
        {
            if (task == null) return;

            try
            {
                await _databaseService.ToggleTaskCompletionAsync(task.Id);
                
                // UI ni yangilash
                var index = Tasks.IndexOf(task);
                if (index >= 0)
                {
                    Tasks[index].IsCompleted = !Tasks[index].IsCompleted;
                    Tasks[index].CompletedAt = Tasks[index].IsCompleted ? DateTime.Now : null;
                }
                
                FilterTasks();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Xatolik", 
                    $"Vazifa holatini o'zgartirishda xatolik: {ex.Message}", "OK");
            }
        }
    }
}