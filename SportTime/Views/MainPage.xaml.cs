using SportTime.ViewModels;

namespace SportTime.Views
{
    /// <summary>
    /// Asosiy sahifa - vazifalar ro'yxati
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadTasksAsync();
        }
    }
}