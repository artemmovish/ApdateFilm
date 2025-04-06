using ApdateFilmUser.Services.API;
using Microsoft.Extensions.Logging;

namespace ApdateFilmUser;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        ApiClient.Initialize("https://d1096a65-54e1-466d-88e3-3fd55ce7bfbd.tunnel4.com");

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
