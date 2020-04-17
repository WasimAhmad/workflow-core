using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.SampleWebApp.Models;

namespace WorkflowCore.SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWorkflowController _workflowController;
        private readonly IWorkflowHost _workflowHost;
        private readonly ISearchIndex _searchIndex;
        private readonly IPersistenceProvider _persistenceProvider;

        public HomeController(ILogger<HomeController> logger,
                                IWorkflowController workflowController,
                                IWorkflowHost workflowHost,
                                ISearchIndex searchIndex,
                                IPersistenceProvider persistenceProvider
                                )
        {
            _logger = logger;
            _workflowController = workflowController;
            _workflowHost = workflowHost;
            _searchIndex = searchIndex;
            _persistenceProvider = persistenceProvider;
        }

        public IActionResult Index()
        {
            //string workflowId = _workflowHost.StartWorkflow("HumanWorkflow").Result;
            //B78F5A297D78394F A18400D66DE2A75D
            var openItems = _workflowHost.GetOpenUserActions("295a8fb7-787d-4f39-a184-00d66de2a75d");
            //await _workflowHost.PublishUserAction(openItems.First().Key, "Waseem", "yes");
            //openItems = _workflowHost.GetOpenUserActions("295a8fb7-787d-4f39-a184-00d66de2a75d");
            return new JsonResult(openItems);
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
