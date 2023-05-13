//using static Android.Telecom.Call;

namespace WorkHiveMobileApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

    }
}
