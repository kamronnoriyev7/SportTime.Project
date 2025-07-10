namespace ToDoListApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Ilovaning asosiy sahifasi sifatida AppShell ni o'rnatish
            MainPage = new AppShell();
        }
    }
}
