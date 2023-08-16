using Microsoft.Extensions.Logging;
using StiQr_SIMTEL.Client.Data;
using StiQR_SIMTEL.Services;

namespace StiQr_SIMTEL.Client
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}