using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class ListController : Controller
    {   // internal = No other assembly can access. it is only for this project.
        internal static Dictionary<string, string> columnChoices = new Dictionary<string, string>();

        // This is a "static constructor" which can be used
        // to initialize static members of a class. // It will run first and only once
        static ListController() 
        {
            
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");
        }
        // route /list/index
        public IActionResult Index()
        {   // dictionary columnChoices was initialize at CLR and is now 
            // added to ViewBag property columns
            ViewBag.columns = columnChoices;
            return View();
        }
        // route /list/Values?column="string"
        public IActionResult Values(string column)
        {
            if (column.Equals("all"))
            {   // Initialize List of Dictionary string string jobs by calling Jobdata.FindAll
                List<Dictionary<string, string>> jobs = JobData.FindAll();
                ViewBag.title =  "All Jobs";
                ViewBag.jobs = jobs;
                return View("Jobs");
            }
            else
            {
                List<string> items = JobData.FindAll(column);
                ViewBag.title =  "All " + columnChoices[column] + " Values";
                ViewBag.column = column;
                ViewBag.items = items;
                return View(); // /Views/List/Values.cshtml
            }
        }

        public IActionResult Jobs(string column, string value)
        {
            List<Dictionary<String, String>> jobs = JobData.FindByColumnAndValue(column, value);
            ViewBag.title = "Jobs with " + columnChoices[column] + ": " + value;
            ViewBag.jobs = jobs;

            return View();
        }
    }
}
