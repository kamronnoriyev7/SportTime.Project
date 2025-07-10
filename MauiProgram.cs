using Microsoft.Extensions.Logging;
using ToDoListApp.Services;
using ToDoListApp.ViewModels;
using ToDoListApp.Views;
using CommunityToolkit.Maui; // CommunityToolkit dan foydalanish uchun

namespace ToDoListApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit() // CommunityToolkit.Mvvm va boshqa yordamchilar uchun
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    // Agar boshqa shriftlar kerak bo'lsa, shu yerga qo'shiladi
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Dependency Injection sozlamalari

            // Servislar
            // DatabaseService ni Singleton sifatida ro'yxatdan o'tkazish,
            // chunki butun ilova bitta ma'lumotlar bazasi instansiyasidan foydalanishi kerak.
            builder.Services.AddSingleton<DatabaseService>();

            // ViewModellar
            // TaskViewModel ni Singleton qilishimiz mumkin, chunki u asosiy ma'lumotlar (vazifalar ro'yxati)
            // bilan ishlaydi va bu ma'lumotlar ilova davomida saqlanishi kerak.
            builder.Services.AddSingleton<TaskViewModel>();

            // EditTaskViewModel har bir EditTaskPage uchun alohida bo'lishi kerak,
            // shuning uchun Transient sifatida ro'yxatdan o'tkazamiz.
            builder.Services.AddTransient<EditTaskViewModel>();

            // Sahifalar (Views)
            // MainPage odatda Singleton bo'ladi, chunki u asosiy sahifa.
            builder.Services.AddSingleton<MainPage>();

            // AddTaskPage va EditTaskPage har safar chaqirilganda yangi instansiya yaratilishi kerak,
            // shuning uchun Transient sifatida ro'yxatdan o'tkazamiz.
            builder.Services.AddTransient<AddTaskPage>();
            builder.Services.AddTransient<EditTaskPage>();

            return builder.Build();
        }
    }
}
