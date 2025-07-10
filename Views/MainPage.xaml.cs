using ToDoListApp.ViewModels;

namespace ToDoListApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly TaskViewModel _viewModel;

        public MainPage(TaskViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // ViewModel'dagi LoadTasksCommand orqali vazifalarni yuklash
            if (_viewModel.LoadTasksCommand.CanExecute(null))
            {
                await _viewModel.LoadTasksCommand.ExecuteAsync(null);
            }
        }
    }
}
