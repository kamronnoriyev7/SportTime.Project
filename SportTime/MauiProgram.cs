using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SportTime.Services;
using SportTime.ViewModels;
using SportTime.Views;
using SportTime.Converters;

namespace SportTime
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Servislarni ro'yxatdan o'tkazish
            builder.Services.AddDbContext<DatabaseService>();
            
            // ViewModellarni ro'yxatdan o'tkazish
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<AddEditTaskViewModel>();
            
            // Sahifalarni ro'yxatdan o'tkazish
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddEditTaskPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
