using ApdateFilmUser.Services.API;
using Microsoft.Extensions.Logging;

namespace ApdateFilmUser;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        ApiClient.Initialize("https://22d111b7-590a-4a57-b34a-31a4c6ed8979.tunnel4.com");

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
