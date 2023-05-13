using Helper;
using Models;
using Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WorkHiveServices.Interface;

namespace WorkHiveServices
{
    public class FreelancerService : IFreelancerService
    {
        public async Task<User> GetFreelancerDetails(string userId)
        {
            User details = new User();
            try
            {
                details = await ApiHelper.GetAsync<User>("api/Users/GetDetails/" + userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return details;
        }
        public async Task<bool> UpdateProfile(User user)
        {
            bool result = false;
            try
            {
                ProfileViewModel profile = new ProfileViewModel();
                profile.UserId = user.Id;
                profile.ProfileId = user.Profile.ProfileId;

                profile.Name = user.UserName;
                profile.Email = user.Email;
                profile.Phone = user.PhoneNumber;
                profile.Location = user.Location;
                profile.ProfileImage = string.IsNullOrEmpty(user.ProfileImage)?string.Empty: user.ProfileImage;
                profile.Skills = !string.IsNullOrEmpty(user.Profile.Skills)? user.Profile.Skills:"";
                profile.Experience = user.Profile.Experience;
                profile.Designation = !string.IsNullOrEmpty(user.Profile.Designation) ? user.Profile.Designation:"";
                profile.Description = !string.IsNullOrEmpty(user.Profile.Description) ?user.Profile.Description:"";
                profile.HourlyRate = user.Profile.HourlyRate;
                profile.LocationCordinates = user.Profile.LocationCordinates;
                profile.ReceiveJobNotifications=user.Profile.ReceiveJobNotifications;
                var vresult = await ApiHelper.PutAsync<bool>("api/Users/UpdateProfile", profile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<List<Bid>> GetBids(string userId)
        {
            List<Bid> result = new List<Bid>();
            try
            {
                var userDetails = await ApiHelper.GetAsync<User>("api/Users/GetDetails/" + userId);
                result = (List<Bid>)userDetails.Bids;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
