using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WorkFlow.Data;
using WorkFlow.Models;


namespace WorkFlow.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public class FlowchartData
        {
            public string Name { get; set; }
            public string JsonData { get; set; }
        }
        public IActionResult Index()
        {
            var userName = User.Identity.Name; // Get the logged-in user's name
            var isAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }
        [HttpPost]
        public IActionResult Save([FromBody] FlowData flowchartData)
        {
          /// var Data = JsonSerializer.Deserialize<FlowData>(flowchartData) ;
           
            // Save the flowchartData to the database or file
            // Example: _flowchartService.SaveFlowchartData(flowchartData);

            return Ok();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
