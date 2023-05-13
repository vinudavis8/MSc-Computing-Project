using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkHiveMobileApp.Services;

namespace WorkHiveMobileApp.ViewModel
{
    //this viewmodel  provides data to MainPage
    public partial class JobsViewModel : ObservableObject
    {


        List<Jobs> jobList = new List<Jobs>();
        public List<Jobs> JobList { get { return jobList; } set => SetProperty(ref jobList, value); }
        public ICommand LoadJobsCommand { get; }
        public JobsViewModel()
        {
            LoadJobsCommand = new Command(async () => await LoadJobsAsync());
        }

        private async Task LoadJobsAsync()
        {
            //data is fetched using api calls in service
            RestService service = new RestService();
            JobList = await service.GetJobList();          
        }
        //checking the internet connectivity
        [RelayCommand]
        async void Add()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {    
            }
            else
            {
                //display a popup
                await Shell.Current.DisplayAlert("Warning", "No Internet", "ok");
            }
        }
        //navigating to jobdetails page on tap
        [RelayCommand]
        async void Tap(int id)
        {
              await Shell.Current.GoToAsync($"JobDetails?JobId={id.ToString()}");

        }
    }
}
