using CommunityToolkit.Mvvm.ComponentModel;
//using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkHiveMobileApp.Services;


namespace WorkHiveMobileApp.ViewModel
{
    //This viewmodel is binded with JobDetails view
    public partial class JobDetailsViewModel: ObservableObject
    {
        Jobs jobList = new Jobs();
        public Jobs JobList { get { return jobList; } set => SetProperty(ref jobList, value); }
        public ICommand LoadJobsCommand { get; }
        public string JobId { get; set; }
        public JobDetailsViewModel()
        
        {
            LoadJobsCommand = new Command(async () => await LoadJobsAsync());
        }
        
        private async Task LoadJobsAsync()
        {
            RestService service = new RestService();
            JobList = await service.GetJobDetails(Convert.ToInt32(this.JobId));
        }
    }
}
