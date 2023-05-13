using System.Runtime.Intrinsics.Arm;
using WorkHiveMobileApp.ViewModel;

namespace WorkHiveMobileApp;
[QueryProperty("JobId", "JobId")] //to receive Job Id parameter from Main page on tap
public partial class JobDetails : ContentPage
{
    public string JobId { get; set; }
    JobDetailsViewModel viewModel;
    public JobDetails(JobDetailsViewModel detailViewModel)
    {
        InitializeComponent();
        viewModel = detailViewModel; //binding viewmodel
    }
    protected override void OnAppearing( )
    {
        base.OnAppearing();
        int.TryParse(JobId, out var result);
        viewModel.JobId = JobId; //setting the Job id from main page 
        BindingContext = viewModel;
        viewModel.LoadJobsCommand.Execute(null); //initiating loading of data
    }

}