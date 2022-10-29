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

        public async Task<IActionResult> Index()
        {
            var result = await _jobManager.GetJobAllJobs();

            return View(result);
        }


        public async Task<IActionResult> ResumeJob(string key)
        {
           await _jobManager.ResumeJob(key);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PauseJob(string key)
        {
            await _jobManager.PauseJob(key);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PauseTrigger(string key)
        {
            await _jobManager.PauseTrigger(key);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ResumeTrigger(string key)
        {
            await _jobManager.ResumeTrigger(key);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteJob(string key)
        {
            await _jobManager.DeleteJob(key);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteTrigger(string key)
        {
            await _jobManager.DeleteTrigger(key);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CreateSchedule()
        {
            await _jobManager.CreateSchedule(typeof(LogJob));
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}