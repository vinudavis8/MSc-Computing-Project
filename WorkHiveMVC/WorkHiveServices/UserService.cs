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
    public class UserService:IUserService
    {
        public async Task<List<User>> GetUsers()
        {
            List<User> usersList=new List<User>();
            try
            {
                usersList = await ApiHelper.GetAsync<List<User>>("api/Users/GetAll");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usersList;
        }
        public async Task<List<User>> GetUsersByRole(string role)
        {
            List<User> usersList = new List<User>();
            try
            {
                usersList = await ApiHelper.GetAsync<List<User>>("api/Users/GetUsersByRole/" + role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usersList;
        }
        public async Task<User> GetUserDetails(string userId)
        {
            User user = new User();
            try
            {
                user = await ApiHelper.GetAsync<User>("api/Users/GetDetails/" + userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }
        public async Task<bool> CheckIfEmailExists(string email)
        {
            try
            {
                var res = await ApiHelper.GetAsync<bool>("api/Users/CheckIfEmailExists/"+ email);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            public async Task<bool> UpdateUser(User user)
        {
            try
            {
                ProfileViewModel profile = new ProfileViewModel();
                profile.UserId = user.Id;
                profile.Name = user.UserName;
                profile.Email = user.Email;
                profile.Phone = user.PhoneNumber;
                profile.Location = user.Location;
                profile.ProfileImage = user.ProfileImage;
                
                var res = await ApiHelper.PutAsync<bool>("api/Users/UpdateUser", profile);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Register(RegisterRequest user)
        {
            try
            {
                var res = await ApiHelper.PostAsync<bool>("api/Users/Register", user);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
        public async Task<LoginResponse> Login(string username,string password)
        {
            try
            {
                var res = await ApiHelper.PostAsync<LoginResponse>("api/Users/login",new  { Username = username ,Password= password });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<bool> ForgotPassword(string email)
        {
            var result = await ApiHelper.PostAsync<bool>("api/Users/ForgotPassword", email);
            return result;
        }
        public async Task<bool> ResetPassword(ResetPasswordRequest reset)
        {
            var res = await ApiHelper.PostAsync<bool>("api/Users/ResetPassowrd", reset);
            return res;
        }

    }
}
