using ZXing.Net.Maui;
using Microsoft.Extensions.Logging;
using StiQr_SIMTEL.Client.Data;
using StiQr_SIMTEL.Client.Services;
using CommunityToolkit.Maui;

namespace StiQr_SIMTEL.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
            #region
                .ConfigureMauiHandlers(h =>
                {
                    h.AddHandler(typeof(ZXing.Net.Maui.Controls.CameraBarcodeReaderView),typeof(CameraBarcodeReaderViewHandler));
                    h.AddHandler(typeof(ZXing.Net.Maui.Controls.BarcodeGeneratorView),typeof(BarcodeGeneratorViewHandler));
                    h.AddHandler(typeof(ZXing.Net.Maui.Controls.CameraView),typeof(CameraViewHandler));
                })
                ;
            #endregion

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IAppService, AppService>();
            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}