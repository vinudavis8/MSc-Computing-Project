//using AndroidX.Lifecycle;
//using AndroidX.Lifecycle;
using WorkHiveMobileApp.ViewModel;

namespace WorkHiveMobileApp;

//This is the code behind file of MainPage
public partial class MainPage : ContentPage
{
    public JobsViewModel viewModel;
    public MainPage(JobsViewModel jobsViewModel)
	{
		InitializeComponent();
        BindingContext = jobsViewModel;
        viewModel = jobsViewModel; //binding the viewmodel to view
        viewModel.LoadJobsCommand.Execute(null); //initiating dataloading
    }
}

