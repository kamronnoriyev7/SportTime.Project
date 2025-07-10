using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoListApp.Models;
using ToDoListApp.Services;
using ToDoListApp.Views; // Sahifalarga navigatsiya uchun

namespace ToDoListApp.ViewModels
{
    public partial class TaskViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        ObservableCollection<TaskModel> _tasks;

        [ObservableProperty]
        string _newTaskTitle;

        [ObservableProperty]
        string _newTaskDescription;

        [ObservableProperty]
        DateTime _newTaskDueDate = DateTime.Today; // Standart qiymat bugungi sana

        public TaskViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Tasks = new ObservableCollection<TaskModel>();
            LoadTasksCommand = new AsyncRelayCommand(LoadTasksAsync);
            AddTaskCommand = new AsyncRelayCommand(AddTaskAsync);
            // EditTaskCommand va DeleteTaskCommand alohida task item bilan bog'liq bo'ladi
            // UpdateTaskStatusCommand task itemdagi checkbox bilan bog'liq bo'ladi
        }

        public IAsyncRelayCommand LoadTasksCommand { get; }
        public IAsyncRelayCommand AddTaskCommand { get; }


        private async Task LoadTasksAsync()
        {
            var tasksFromDb = await _databaseService.GetTasksAsync();
            Tasks.Clear();
            foreach (var task in tasksFromDb)
            {
                Tasks.Add(task);
            }
        }

        private async Task AddTaskAsync()
        {
            if (string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                // Foydalanuvchiga xabar ko'rsatish (masalan, DisplayAlert orqali)
                await Shell.Current.DisplayAlert("Xatolik", "Vazifa sarlavhasi bo'sh bo'lishi mumkin emas.", "OK");
                return;
            }

            var newTask = new TaskModel
            {
                Title = NewTaskTitle,
                Description = NewTaskDescription,
                DueDate = NewTaskDueDate,
                IsCompleted = false
            };

            await _databaseService.SaveTaskAsync(newTask);

            // Formani tozalash
            NewTaskTitle = string.Empty;
            NewTaskDescription = string.Empty;
            NewTaskDueDate = DateTime.Today;

            // Ro'yxatni yangilash
            await LoadTasksAsync();

            // Agar AddTaskPage ishlatilsa, u yerdan orqaga qaytish kerak
            if (Shell.Current.CurrentPage is AddTaskPage)
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async Task GoToAddTaskPageAsync()
        {
            // Yangi vazifa qo'shish sahifasiga o'tish
            // Bu yerda NewTaskTitle, NewTaskDescription, NewTaskDueDate ni tozalash mumkin
            // yoki AddTaskPage o'zining ViewModeliga ega bo'lishi mumkin.
            // Hozircha soddalik uchun shu ViewModel'dan foydalanamiz.
            NewTaskTitle = string.Empty;
            NewTaskDescription = string.Empty;
            NewTaskDueDate = DateTime.Today;
            await Shell.Current.GoToAsync(nameof(AddTaskPage));
        }

        [RelayCommand]
        private async Task GoToEditTaskPageAsync(TaskModel taskToEdit)
        {
            if (taskToEdit == null)
                return;

            // Tahrirlash sahifasiga parametr bilan o'tish
            // EditTaskPage o'z ViewModeliga ega bo'lishi yoki parametrni qabul qilib
            // shu TaskViewModel'dagi biror xususiyatga yuklashi mumkin.
            // Hozircha navigatsiya parametrini ishlatamiz.
            var navigationParameter = new Dictionary<string, object>
            {
                { "TaskToEdit", taskToEdit }
            };
            await Shell.Current.GoToAsync(nameof(EditTaskPage), navigationParameter);
        }

        [RelayCommand]
        private async Task DeleteTaskAsync(TaskModel taskToDelete)
        {
            if (taskToDelete == null)
                return;

            bool confirmed = await Shell.Current.DisplayAlert("Tasdiqlash", $"'{taskToDelete.Title}' vazifasini o'chirishni xohlaysizmi?", "Ha", "Yo'q");
            if (confirmed)
            {
                await _databaseService.DeleteTaskAsync(taskToDelete);
                Tasks.Remove(taskToDelete); // UI dan darhol olib tashlash
                // Yoki LoadTasksAsync() ni chaqirish mumkin, lekin bu optimallashtirilmagan bo'ladi
            }
        }

        [RelayCommand]
        private async Task ToggleTaskCompletionAsync(TaskModel task)
        {
            if (task == null)
                return;

            task.IsCompleted = !task.IsCompleted;
            await _databaseService.SaveTaskAsync(task);

            // UI da o'zgarishni aks ettirish uchun (agar CollectionView item binding to'g'ri bo'lsa kerak emas)
            // Yoki butun ro'yxatni qayta yuklash mumkin, lekin bu ham optimallashtirilmagan
            // Eng yaxshisi TaskModel da INotifyPropertyChanged ni implement qilish yoki
            // TaskModel ni o'rab turuvchi kichik ViewModel yaratish.
            // Hozircha soddalik uchun shunday qoldiramiz. Ro'yxatni yangilash uchun:
            // LoadTasksAsync() ni chaqirish mumkin, lekin bu UI da miltillashga olib kelishi mumkin.
            // Yaxshiroq yechim:
            int index = Tasks.IndexOf(task);
            if (index != -1)
            {
                Tasks[index] = task; // Bu ObservableCollection uchun CollectionChanged hodisasini ishga tushiradi
            }
        }
    }
}
