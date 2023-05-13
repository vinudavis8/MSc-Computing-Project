using Helper;
using Models;
using Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WorkHiveServices.Interface;

namespace WorkHiveServices
{
    public class JobService:IJobService
    {
        public async Task<List<Job>> GetJobs(JobSearchParams searchParams)
        {
            List<Job> jobList=new List<Job>();
            try
            {
                
                    jobList = await ApiHelper.GetAsync<List<Job>>("api/Jobs?SearchLocation=" + searchParams.SearchLocation
                        + "&SearchTitle=" + searchParams.SearchTitle + "&SearchCategory=" + searchParams.SearchCategory + "");
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return jobList;
        }
        public async Task<Job> GetJobDetails(int jobId)
        {
            Job jobDetails = new Job();
            try
            {
                jobDetails = await ApiHelper.GetAsync<Job>("api/Jobs/GetDetails/" + jobId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return jobDetails;
        }
        public async Task<Job> CreateJob(JobRequest job)
        {
            Job jobDetails = new Job();
            try
            {
                jobDetails = await ApiHelper.PostAsync<Job>("api/Jobs", job);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return jobDetails;
        }
        public async Task<bool> UpdateJob(Job job)
        {
            try
            {
                UpdateJobRequest obj = new UpdateJobRequest
                {
                    JobId=job.JobId,
                    Title = job.Title,
                    Budget = job.Budget,
                    Deadline = job.Deadline,
                    Description = job.Description,
                    JobType = job.JobType,
                    Location = job.Location,
                    SkillTags = job.SkillTags,
                };
                var result = await ApiHelper.PutAsync<Job>("api/Jobs", obj);
                if (result != null)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteJob(int jobId)
        {
            try
            {
                var result = await ApiHelper.DeleteAsync<bool>("api/Jobs/"+ jobId);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> SaveBid(BidRequest bid)
        {
            try
            {
                var result = await ApiHelper.PostAsync<bool>("api/Bids", bid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public async Task<List<BidsViewModel>> GetBids(string userId)
        {
            List<BidsViewModel> result = new List<BidsViewModel>();
            try
            {
                var user = await ApiHelper.GetAsync<User>("api/Users/GetDetails/" + userId);
                var userList = await ApiHelper.GetAsync<List<User>>("api/Users/GetAll");
                foreach (var job in user.Jobs)
                {
                    var bids = job.Bids;
                    foreach (var bid in bids)
                    {
                        var c = userList.Where(u => u.Bids.Any(b => b.BidId == bid.BidId)).Select(u => u).FirstOrDefault();

                        BidsViewModel vm = new BidsViewModel();
                        vm.Status = bid.Status;
                        vm.FreelancerName = c.UserName;
                        vm.FreelancerId = c.Id;
                        vm.JobName = job.Title;
                        vm.BidAmount = bid.BidAmount;
                        vm.ExpectedDate = bid.ExpectedDate;
                        vm.BidId = bid.BidId;
                        vm.Description = bid.Description;
                        result.Add(vm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<bool> UpdateBidStatus(int bidId)
        {
            bool result = false;
            try
            {
                result = await ApiHelper.PostAsync<bool>("api/Bids/UpdateBidStatus/", bidId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
