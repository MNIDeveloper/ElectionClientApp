using CommunityToolkit.Maui;
using ElectionApp.Interfaces;
using ElectionApp.Services;
using ElectionApp.Views;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace ElectionApp;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<ManualMainPage>();
        builder.Services.AddTransient<MenuPage>();
        builder.Services.AddTransient<ElectionPage>();
        builder.Services.AddTransient<IApiServices, ElectionAPIService>();
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
