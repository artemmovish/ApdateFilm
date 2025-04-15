using ApdateFilmUser.Services.API;
using Microsoft.Extensions.Logging;

namespace ApdateFilmUser;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        ApiClient.Initialize("https://66f0e5c5-781d-47de-95f6-610cb04e0991.tunnel4.com");

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

	public static async Task<int> GG()
	{
		return await GG1();
	}

    public static async Task<int> GG1()
    {
        return 1;
    }
}
