using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }
        // TODO #1 - Create a Results action method to process 
        // search request and display results

        public IActionResult Results(string searchType, string searchTerm)
        {
            ViewBag.columns = ListController.columnChoices;
            if (string.IsNullOrEmpty(searchTerm)|| string.IsNullOrEmpty(searchType))
            {
                ViewBag.error = "Please enter keyword.";
                return View("Views/Search/Results.cshtml");
            }
            
            List<Dictionary<string, string>> matchedJob = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> allJobs = JobData.FindAll();
            string lowerTerm = searchTerm.ToLower();
            string lowerType = searchType.ToLower();
            if (searchType.Equals("all"))
            {
                
                foreach (Dictionary<string,string> listing in allJobs)
                {
                    foreach (KeyValuePair<string,string> property in listing)
                    {
                        if (property.Key.ToLower().Contains(lowerTerm) || property.Value.ToLower().Contains(lowerTerm))
                        {
                            matchedJob.Add(listing);
                            break;
                        }
                    }
                }
            }
            else
            {
                matchedJob = JobData.FindByColumnAndValue(lowerType, lowerTerm);
            }
            ViewBag.searchTerm = searchTerm;
            ViewBag.searchType = searchType;
            ViewBag.title = "Search Result";
            ViewBag.jobs = matchedJob;
            return View("Views/Search/Results.cshtml");
        }

    }
}
