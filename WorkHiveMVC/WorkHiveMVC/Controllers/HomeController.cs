using Microsoft.AspNetCore.Mvc;
using WorkHiveServices.Interface;
using WorkHiveServices;
using Helper;
using Models;
using Microsoft.CodeAnalysis;

namespace WorkHiveMVC.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger,IHomeService homeService)
        {
            _homeService = homeService;
            _logger = logger;
        }

        //Main entry point of the application which is the landing page
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await _homeService.GetDashboardData();
                return View("LandingPage", data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occured");
                return RedirectToAction("Error", "Home", new { ex = ex});
            }
        }

        //generic action methord.
        //every catch block is redirected to this action methord
        //error logging is implemented here using serilog
        public ActionResult Error(Exception ex)
        {
            _logger.LogError(ex, "Error occured");
            return View();
        }
    }
}
