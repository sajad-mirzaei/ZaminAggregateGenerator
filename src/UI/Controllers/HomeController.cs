using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using UI.Models;
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
                CC(aggregateGeneratorModel.AggregateClass);
                return View(oAggregateGeneratorModel);
            }
            //oAggregateGenerator.Run();
            return View(new AggregateGeneratorModel());
        }
        static void CC(string input)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(input);
            var root = syntaxTree.GetRoot();

            var classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
            if (classNode != null)
            {
                var className = classNode.Identifier.ValueText;

                var properties = classNode.DescendantNodes().OfType<PropertyDeclarationSyntax>();
                foreach (var property in properties)
                {
                    var propertyName = property.Identifier.ValueText.ToString();
                    var propertyType = property.Type.ToString();

                    Console.WriteLine($"var p = {className}.{propertyName};");
                    Console.WriteLine($"var pType = {propertyType};");
                }
            }
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