using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TransUploaderWebService.Models;
using TransUploaderWebService.Services.Interface;

namespace TransUploaderWebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IDataProcessorFactory processorFactory;

        public HomeController(ILogger<HomeController> _logger, IDataProcessorFactory _processorFactory)
        {
            logger = _logger;
            processorFactory = _processorFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(UploaderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string filetype = Path.GetExtension(model.UploadFile.FileName);
                    using (var processor = processorFactory.CreateDataProcessor(filetype, model.UploadFile.OpenReadStream()))
                    {
                        if (processor != null)
                        {
                            ViewBag.Message = processor.ProcessRequest();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Exception throws for {ex.Message}");
                ViewBag.Message = "Something went wrong!!";

                return new BadRequestObjectResult(new
                {
                    message = ex.Message
                });

            }

            return View();
        }

        public IActionResult Privacy()
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