using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModel;
using System.Data;
using WorkHiveServices;
using WorkHiveServices.Interface;

namespace WorkHiveMVC.Controllers
{
    //this controller has the client related features
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;


        public ClientController( IJobService jobService, IUserService userService, ICategoryService categoryService)
        {
            _jobService = jobService;
            _userService = userService;
            _categoryService = categoryService;
        }
        public async Task<ActionResult> Bids()
        {
            try
            {
                //session Id  stored in session after login is fetched
                string userId = HttpContext.Session.GetString("loggedInUserId");
                var bidList = await _jobService.GetBids(userId);
                return View(bidList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }
        public async Task<ActionResult> MyJobs()
        {
            try
            {
                string userId = HttpContext.Session.GetString("loggedInUserId");
                JobSearchParams param = new JobSearchParams();
                param.ClientID = userId;
                var jobsList = await _jobService.GetJobs(param);
                return View(jobsList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }
        public async Task<ActionResult> CreateJob()
        {
            try
            {
                List<Category> categoryList = await _categoryService.GetCategory();
                List<SelectListItem> items = new List<SelectListItem>();
                //to populate category dropdown in the create job page
                foreach (var cat in categoryList)
                {
                    items.Add(new SelectListItem { Value = cat.CategoryId.ToString(), Text = cat.CategoryName });
                }
                ViewBag.Category = items;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }
        [HttpPost]
        public async Task<ActionResult> SaveJob(JobRequest job)
        {
            try
            {
                string userId = HttpContext.Session.GetString("loggedInUserId");
                job.UserId = userId;
                job.DatePosted = DateTime.Now;
                var user = await _jobService.CreateJob(job);
                return RedirectToAction("Myjobs");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditJob(int jobId)
        {
            try
            {
                var jobDetails = await _jobService.GetJobDetails(jobId);
                return View(jobDetails);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditJob(Job jobDetails)
        {
            try
            {
                var result = await _jobService.UpdateJob(jobDetails);
                //setting the message in a viewbag to show in the view
                if (result)
                    ViewData["message"] = "Job Updated Successfully";
                else
                    ViewData["message"] = "Update Failed. Please Try Again.";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }


        public async Task<ActionResult> DeleteJob(int JobId)
        {
            try
            {
                var result = await _jobService.DeleteJob(JobId);
                //setting the message in a viewbag to show in the view
                if (result)
                    ViewBag.message = "Job Deleted Successfully";
                else
                    ViewBag.message = "Delete Failed. Please Try Again.";
                return RedirectToAction("MyJobs");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }

        public async Task<ActionResult> EditProfile()
        {
            try
            {
                string userId = HttpContext.Session.GetString("loggedInUserId");
                var user = await _userService.GetUserDetails(userId);
                return View(user);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditProfile(User user, IFormFile fileUpload)
        {
            try
            {
                //if user upload image, convert into base64 and save in database
                if (fileUpload != null && fileUpload.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await fileUpload.CopyToAsync(memoryStream);
                        var imageData = memoryStream.ToArray();
                        var base64String = Convert.ToBase64String(imageData);
                        user.ProfileImage = base64String;
                    }
                }
                var details = await _userService.UpdateUser(user);
                return RedirectToAction("EditProfile");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }

        public async Task<bool> UpdateBidStatus(int bidId)
        {
            //bid status will be updated to accepted
            var jobsList = await _jobService.UpdateBidStatus(bidId);
            return true;
        }
    }
}
