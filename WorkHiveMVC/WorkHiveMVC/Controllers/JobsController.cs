using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Models;
using Models.ViewModel;
using System.Data;
using WorkHiveServices;
using WorkHiveServices.Interface;
using X.PagedList;
using Microsoft.CognitiveServices.Speech;

namespace WorkHiveMVC.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobService _jobService;
        private readonly ICategoryService _categoryService;
        public JobsController(IJobService jobService, ICategoryService categoryService)
        {
            _jobService = jobService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> JobSearch(string? searchLocation, string? searchTitle, string? SearchCategory)
        {
            //this action methord is used in landing page
            try
            {
                List<Category> categoryList = await _categoryService.GetCategory();
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Value = "", Text = "All" });
                foreach (var cat in categoryList)
                {
                    items.Add(new SelectListItem { Value = cat.CategoryId.ToString(), Text = cat.CategoryName });
                }
                ViewBag.Category = items;
                ViewBag.SearchTitle = !string.IsNullOrEmpty(searchTitle) ? searchTitle : "";
                ViewBag.SearchLocation = !string.IsNullOrEmpty(searchLocation) ? searchLocation : "";
                ViewBag.JobType = "";
                ViewBag.SearchCategory = !string.IsNullOrEmpty(SearchCategory) ? SearchCategory : "";

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }
        [HttpPost]
        public async Task<IActionResult> Search([FromBody] JobSearchParams searchParams, int? page)
        {
            //this action methord is used in job search page
            try
            {
                if (searchParams == null)
                { searchParams = new JobSearchParams(); }
                var jobsList = await _jobService.GetJobs(searchParams);
                ViewBag.SearchTitle = searchParams.SearchTitle;
                ViewBag.SearchLocation = searchParams.SearchLocation;
                ViewBag.SearchCategory = searchParams.SearchCategory;
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return PartialView("_PartialViewJobCard", jobsList.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }

        public async Task<IActionResult> JobDetails(int jobId)
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

        public async Task<IActionResult> CreateJob()
        {
            return View();
        }
        [HttpPost]
        public async Task<bool> SaveBid([FromBody] BidRequest bid)
        {
            try
            {
                string userId = HttpContext.Session.GetString("loggedInUserId");
                bid.UserId = userId;
                var result = await _jobService.SaveBid(bid);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

 
    }
}
