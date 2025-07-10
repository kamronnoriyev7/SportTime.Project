using System.Windows.Input;
using SportTime.Models;
using SportTime.Services;

namespace SportTime.ViewModels
{
    /// <summary>
    /// Vazifa qo'shish va tahrirlash sahifasi uchun ViewModel
    /// </summary>
    [QueryProperty(nameof(Task), "Task")]
    public class AddEditTaskViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        
        private TaskModel? _task;
        public TaskModel? Task
        {
            get => _task;
            set
            {
                SetProperty(ref _task, value);
                if (value != null)
                {
                    LoadTaskData();
                }
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private DateTime _dueDate = DateTime.Today.AddDays(1);
        public DateTime DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        private bool _hasDueDate;
        public bool HasDueDate
        {
            get => _hasDueDate;
            set => SetProperty(ref _hasDueDate, value);
        }

        public bool IsEditing => Task != null;
        public string PageTitle => IsEditing ? "Vazifani tahrirlash" : "Yangi vazifa";
        public string SaveButtonText => IsEditing ? "Yangilash" : "Saqlash";

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditTaskViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            
            SaveCommand = new Command(async () => await SaveTaskAsync(), CanSave);
            CancelCommand = new Command(async () => await CancelAsync());
        }

        /// <summary>
        /// Mavjud vazifa ma'lumotlarini yuklash (tahrirlash rejimida)
        /// </summary>
        private void LoadTaskData()
        {
            if (Task == null) return;

            Title = Task.Title;
            Description = Task.Description;
            HasDueDate = Task.DueDate.HasValue;
            DueDate = Task.DueDate ?? DateTime.Today.AddDays(1);
        }

        /// <summary>
        /// Saqlash tugmasi faol bo'lish sharti
        /// </summary>
        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        /// <summary>
        /// Vazifani saqlash yoki yangilash
        /// </summary>
        private async Task SaveTaskAsync()
        {
            if (IsBusy || !CanSave()) return;

            try
            {
                IsBusy = true;

                if (IsEditing && Task != null)
                {
                    // Mavjud vazifani yangilash
                    Task.Title = Title.Trim();
                    Task.Description = Description.Trim();
                    Task.DueDate = HasDueDate ? DueDate : null;
                    
                    await _databaseService.UpdateTaskAsync(Task);
                }
                else
                {
                    // Yangi vazifa yaratish
                    var newTask = new TaskModel
                    {
                        Title = Title.Trim(),
                        Description = Description.Trim(),
                        DueDate = HasDueDate ? DueDate : null,
                        CreatedAt = DateTime.Now
                    };
                    
                    await _databaseService.AddTaskAsync(newTask);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Xatolik", 
                    $"Vazifani saqlashda xatolik: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Bekor qilish va orqaga qaytish
        /// </summary>
        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}