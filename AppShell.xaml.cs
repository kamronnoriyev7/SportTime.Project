using ToDoListApp.Views;

namespace ToDoListApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Navigatsiya uchun sahifalarni ro'yxatdan o'tkazish
            Routing.RegisterRoute(nameof(AddTaskPage), typeof(AddTaskPage));
            Routing.RegisterRoute(nameof(EditTaskPage), typeof(EditTaskPage));
            // MainPage uchun marshrut AppShell.xaml da ShellContent orqali belgilangan,
            // lekin agar kerak bo'lsa, bu yerda ham qo'shimcha qilib qo'shish mumkin:
            // Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
