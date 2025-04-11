using ApdateFilmUser.Services.API;
using Microsoft.Extensions.Logging;

namespace ApdateFilmUser;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        ApiClient.Initialize("https://5b69a80d-7039-4b58-8420-a8e68c2d95f9.tunnel4.com");

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
