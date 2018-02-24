using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TechJobs.Controllers
{   // extending the Controller class
    public class HomeController : Controller
    {   // Route "/" and "Home/Index"
        public IActionResult Index()
        {
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");
            // adding dictionary actionChoices to ViewBag property. An Object with Dynamic Property
            // Within the dynamic Property "actions" is a data holder that implicitly declare type
            ViewBag.actions = actionChoices;
            // return Views(Views/Home/Index.cshtml)
            return View();
        }
    }
}
