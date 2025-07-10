using SportTime.Services;

namespace SportTime
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            
            // Ma'lumotlar bazasini ishga tushirish
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            try
            {
                var databaseService = new DatabaseService();
                await databaseService.InitializeDatabaseAsync();
            }
            catch (Exception ex)
            {
                // Xatolikni log qilish yoki foydalanuvchiga ko'rsatish
                System.Diagnostics.Debug.WriteLine($"Ma'lumotlar bazasini ishga tushirishda xatolik: {ex.Message}");
            }
        }
    }
}
