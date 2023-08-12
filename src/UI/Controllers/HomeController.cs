using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;
using ZaminAggregateGenerator;
using ZaminAggregateGenerator.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            AggregateGeneratorModel model = new AggregateGeneratorModel();
            return View(model);
            //return View();
        }

        [HttpPost]
        //public IActionResult Index(string aggregatePlural, string aggregateName, string projectName, string ProjectPath)
        public IActionResult Index(AggregateGeneratorModel aggregateGeneratorModel)
        {
            if (ModelState.IsValid)
            {
                AggregateGeneratorModel oAggregateGeneratorModel = new AggregateGeneratorModel()
                {
                    AggregatePlural = aggregateGeneratorModel.AggregatePlural,
                    AggregateName = aggregateGeneratorModel.AggregateName,
                    ProjectName = aggregateGeneratorModel.ProjectName,
                    ProjectPath = aggregateGeneratorModel.ProjectPath,
                    AggregateClass = aggregateGeneratorModel.AggregateClass
                };
                AggregateGenerator oAggregateGenerator = new AggregateGenerator(oAggregateGeneratorModel);
                oAggregateGenerator.Generate();
                return View(oAggregateGeneratorModel);
            }
            return View(new AggregateGeneratorModel());
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