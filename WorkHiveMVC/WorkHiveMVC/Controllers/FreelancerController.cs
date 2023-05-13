using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using WorkHiveServices;
using WorkHiveServices.Interface;
using Microsoft.AspNetCore.Http;
using Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WorkHiveMVC.Controllers
{
    //only Freelancers will be able to access these action methords
    
    public class FreelancerController : Controller
    {
        string userId = "";
        private readonly IFreelancerService _freelancerService;
        private readonly IUserService _userservice;
        private readonly IReviewService _reviewService;

        public FreelancerController(IFreelancerService freelancerService, IUserService userService, IHttpContextAccessor httpContextAccessor, IReviewService reviewService)
        {
            _freelancerService = freelancerService;
            _userservice = userService;
            _reviewService = reviewService;
        }

        public async Task<ActionResult> FreelancersMapView()
        {
            return View();
        }
        public async Task<ActionResult> Profile(string? userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId)) 
                    userId = HttpContext.Session.GetString("loggedInUserId");
                var details = await _freelancerService.GetFreelancerDetails(userId);
                return View(details);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }
        [Authorize]
        public async Task<bool> SaveReview([FromBody] ReviewRequest review)
        {
            userId = HttpContext.Session.GetString("loggedInUserId");
            review.ClientId = userId;
            var result = await _reviewService.CreateReview(review);
            return result;
        }
        public async Task<List<object>> GetFreelancers()
        {
            List<object> list = new List<object>();

            try
            {
                var usersList = await _userservice.GetUsersByRole("Freelancer");
                foreach (var user in usersList)
                {
                    //creating an object with location cordinates to set in map view
                    string cordinates = user.Profile != null ? user.Profile.LocationCordinates : "";
                    if (!string.IsNullOrEmpty(cordinates))
                    {
                        string[] location = cordinates.Split(",");
                        object o = new
                        {
                            id = user.Id,
                            name = user.UserName,
                            cordinates = cordinates,
                            lat = Convert.ToDecimal(location[0]),
                            lng = Convert.ToDecimal(location[1])
                        };
                        list.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        [Authorize(Roles = "Freelancer")]
        [HttpGet]
        public async Task<ActionResult> EditProfile()
        {
            try
            {
                userId = HttpContext.Session.GetString("loggedInUserId");
                var profile = await _freelancerService.GetFreelancerDetails(userId);
                return View(profile);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }
        [Authorize(Roles = "Freelancer")]
        [HttpPost]
        public async Task<ActionResult> Edit(User user, IFormFile fileUpload)
        {
            try

            {
                //if  user upload image, convert into base 64 and store in database
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
                var details = await _freelancerService.UpdateProfile(user);
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                return RedirectToAction("EditProfile");
            }
        }
        [Authorize(Roles = "Freelancer")]
        public async Task<ActionResult> ViewProposals()
        {
            try
            {
                userId = HttpContext.Session.GetString("loggedInUserId");
                var details = await _freelancerService.GetBids(userId);
                return View(details);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }

    }
}
