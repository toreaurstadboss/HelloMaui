using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Maui;
using HelloMaui.Demos.CollectionViews;

namespace HelloMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()  
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                })
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("RobotoFlex.ttf", "RobotoFlex");
                    fonts.AddFont("RogueScript.ttf", "RogueScript");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<App>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainMenuPage>();
            builder.Services.AddTransient<ListPage>();

            return builder.Build();
        }
    }
}
