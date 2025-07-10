using ToDoListApp.ViewModels;
using ToDoListApp.Models;

namespace ToDoListApp.Views
{
    // QueryProperty atributi ViewModel darajasida (EditTaskViewModel) ishlatilgani uchun bu yerda kerak emas.
    // Agar parametrni to'g'ridan-to'g'ri Page'ga olish kerak bo'lsa, bu yerda ham qo'llash mumkin.
    public partial class EditTaskPage : ContentPage
    {
        public EditTaskPage(EditTaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        // Agar ViewModel'da QueryProperty ishlatilsa, bu yerda parametrni olish shart emas.
        // ViewModel avtomatik tarzda TaskToEdit xususiyatini to'ldiradi.

        // Misol uchun, agar parametrni shu sahifada olish kerak bo'lsa:
        // public TaskModel TaskToEdit { get; set; }
        // public EditTaskPage(EditTaskViewModel viewModel)
        // {
        //     InitializeComponent();
        //     BindingContext = viewModel;
        //
        //     // Agar TaskToEdit ni bu yerda set qilmoqchi bo'lsak:
        //     // QueryProperty bu sahifaga qo'shilishi kerak va OnNavigatedTo da
        //     // viewModel.TaskToEdit = this.TaskToEdit; kabi o'rnatilishi mumkin.
        //     // Lekin ViewModel darajasidagi QueryProperty ancha qulayroq.
        // }
    }
}
