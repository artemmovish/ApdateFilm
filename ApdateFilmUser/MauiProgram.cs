using ApdateFilmUser.Services.API;
using Microsoft.Extensions.Logging;

namespace ApdateFilmUser;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        ApiClient.Initialize("https://d1df9152-68fa-4acc-b930-4677aabf2728.tunnel4.com");

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
