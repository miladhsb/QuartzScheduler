using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using QuartzSample.Jobs;
using QuartzSample.Models;
using QuartzSample.Services;

namespace QuartzSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJobManager _jobManager;

        public HomeController(ILogger<HomeController> logger, IJobManager jobManager)
        {
            _logger = logger;
            this._jobManager = jobManager;
        }

        public IActionResult Index()
        {


            return View();
        }

        public async Task<IActionResult> StartJob()
        {
           await _jobManager.CreateSchedule(typeof(LogJob));
            return View();
        }
        public async Task<IActionResult> StoptJob()
        {
          await  _jobManager.StopScheduleWithTrigger(typeof(LogJob));
            return View();
        }
        public async Task<IActionResult> restartSchedule()
        {
          
              await  _jobManager.restartSchedule(typeof(LogJob));
           
           
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}