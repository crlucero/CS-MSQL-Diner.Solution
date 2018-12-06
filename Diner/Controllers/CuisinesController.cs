using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Diner.Models;
using System;

namespace Diner.Controllers
{
    public class CuisinesController : Controller
    {
    
        [HttpGet("/cuisines")]
            public ActionResult Index()
            {
                List<Cuisine> allCuisines = Cuisine.GetAll();
                return View(allCuisines);
            }

            [HttpGet("/cuisines/new")]
            public ActionResult New()
            {
                return View();
            }

        [HttpPost("/cuisines")]
        public ActionResult Create(string cuisineName)
        {
        Cuisine newCuisine = new Cuisine(cuisineName);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View("Index", allCuisines);
        }

        [HttpGet("/cuisines/{id}")]
        public ActionResult Show(int id)
        {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine selectedCuisine = Cuisine.Find(id);
        List<Joint> cuisineJoints = selectedCuisine.GetJoints();
        model.Add("cuisine", selectedCuisine);
        model.Add("joints", cuisineJoints);
        return View(model);
        }

        //Adds new joints to cuisine
        [HttpPost("/cuisines/{cuisineId}")]
        public ActionResult Create(string jointName, int cuisineId, string jointNotes)
        {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine foundCuisine = Cuisine.Find(cuisineId);
        Joint newJoint = new Joint(jointName, cuisineId, jointNotes);
        newJoint.Save();
        List<Joint> cuisineJoints = foundCuisine.GetJoints();
        model.Add("joints", cuisineJoints);
        model.Add("cuisine", foundCuisine);
        return View("Show", model);
        }
        


    }
}