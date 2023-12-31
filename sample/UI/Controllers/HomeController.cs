﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;
using ZaminAggregateGenerator;
using ZaminAggregateGenerator.Models;

namespace UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        IndexViewModel indexViewModel = new()
        {
            AggregateGeneratorModel = new AggregateGeneratorModel()
            {
                AggregateClass = "class SampleClass {\n    public int P1 { get; set; }\n    public string P2 { get; set; }\n}"
            }
        };
        return View(indexViewModel);
    }

    [HttpPost]
    public IActionResult Index(AggregateGeneratorModel aggregateGeneratorModel)
    {
        IndexViewModel indexViewModel = new()
        {
            FormMessage = "مشکلی وجود دارد",
            FormSubmit = true,
            FormValidation = false
        };
        if (ModelState.IsValid)
        {
            AggregateGeneratorModel oAggregateGeneratorModel = new()
            {
                AggregatePlural = aggregateGeneratorModel.AggregatePlural,
                AggregateName = aggregateGeneratorModel.AggregateName,
                ProjectName = aggregateGeneratorModel.ProjectName,
                ProjectPath = aggregateGeneratorModel.ProjectPath,
                AggregateClass = aggregateGeneratorModel.AggregateClass
            };
            AggregateGenerator oAggregateGenerator = new(oAggregateGeneratorModel);
            oAggregateGenerator.Generate();
            indexViewModel.FormMessage = "فایل ها با موفقیت ساخته شدند";
            indexViewModel.FormValidation = true;
            indexViewModel.AlertClass = "alert alert-success";
            indexViewModel.AggregateGeneratorModel = oAggregateGeneratorModel;
        }
        return View(indexViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}