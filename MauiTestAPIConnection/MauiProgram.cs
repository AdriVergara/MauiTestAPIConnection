using MauiTestAPIConnection.Interfaces;
using MauiTestAPIConnection.Services;
using Microsoft.Extensions.Logging;

namespace MauiTestAPIConnection;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        //builder.RegisterViews(); //= RegisterAppServices(builder);

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ViewModels.MainPageViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IRestService, RestService>();
        // Omitted for brevity...
        return mauiAppBuilder;

    }
}
