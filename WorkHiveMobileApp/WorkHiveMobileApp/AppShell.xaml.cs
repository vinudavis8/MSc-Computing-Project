//using static Android.Telecom.Call;

namespace WorkHiveMobileApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("JobDetails", typeof(JobDetails));

    }

}
