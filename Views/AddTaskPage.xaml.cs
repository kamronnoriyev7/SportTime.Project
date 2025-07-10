using ToDoListApp.ViewModels;

namespace ToDoListApp.Views
{
    public partial class AddTaskPage : ContentPage
    {
        // TaskViewModel ni MainPage dan yoki DI orqali olishimiz mumkin.
        // Hozircha DI orqali olishni nazarda tutamiz, MauiProgram.cs da ro'yxatdan o'tkaziladi.
        public AddTaskPage(TaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            // Orqaga qaytish
            if (Shell.Current.CurrentState.Location.OriginalString.Contains(nameof(AddTaskPage)))
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        // Sahifa ko'ringanda ViewModel dagi eski ma'lumotlarni tozalash (ixtiyoriy)
        // protected override void OnAppearing()
        // {
        //     base.OnAppearing();
        //     var vm = BindingContext as TaskViewModel;
        //     if (vm != null)
        //     {
        //         vm.NewTaskTitle = string.Empty;
        //         vm.NewTaskDescription = string.Empty;
        //         vm.NewTaskDueDate = DateTime.Today;
        //     }
        // }
    }
}
