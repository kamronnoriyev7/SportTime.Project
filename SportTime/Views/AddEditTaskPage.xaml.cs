using SportTime.ViewModels;

namespace SportTime.Views
{
    /// <summary>
    /// Vazifa qo'shish va tahrirlash sahifasi
    /// </summary>
    public partial class AddEditTaskPage : ContentPage
    {
        public AddEditTaskPage(AddEditTaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}