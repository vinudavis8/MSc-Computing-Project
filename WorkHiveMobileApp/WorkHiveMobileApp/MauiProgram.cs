using WorkHiveMobileApp.ViewModel;
//using static Android.Telecom.Call;

namespace WorkHiveMobileApp;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		//adding the services
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<JobsViewModel>();
        builder.Services.AddTransient<JobDetails>();
        builder.Services.AddTransient<JobDetailsViewModel>();
       
        return builder.Build();
	}
}
