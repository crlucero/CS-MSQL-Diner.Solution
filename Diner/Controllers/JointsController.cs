using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Diner.Models;
using System;

namespace Diner.Controllers
{
    public class JointsController : Controller
    {
    
        [HttpGet("/cuisines/{cuisineId}/joints/new")]
        public ActionResult New(int cuisineId)
        {
        Cuisine cuisine = Cuisine.Find(cuisineId);
        return View(cuisine);
        }
        
        [HttpGet("/cuisines/{cuisineId}/joints/{jointId}")]
        public ActionResult Show(int cuisineId, int jointId)
        {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Joint joint = Joint.Find(jointId);
        Cuisine cuisine = Cuisine.Find(cuisineId);
        model.Add("joint", joint);
        model.Add("cuisine", cuisine);
        return View(model);
        }

        [HttpGet("/cuisines/{cuisineId}/joints/{jointId}/edit")]
        public ActionResult Edit(int cuisineId, int jointId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Cuisine cuisine = Cuisine.Find(cuisineId);
            model.Add("cuisine", cuisine);
            Joint joint = Joint.Find(jointId);
            model.Add("joint", joint);
            return View(model);
        }

        [HttpPost("/cuisines/{cuisineId}/joints/{jointId}")]
        public ActionResult Update(int cuisineId, int jointId, string newName, string newNotes)
        {
        Joint joint = Joint.Find(jointId);
        joint.Edit(newName, newNotes);
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine cuisine = Cuisine.Find(cuisineId);
        model.Add("cuisine", cuisine);
        model.Add("joint", joint);
        return View("Show", model);
        }

        [HttpDelete("/cuisines/{cuisineId}/joints/{jointId}/delete")]
        public ActionResult Delete(int jointId, int cuisineId)
        {
            Joint joint = Joint.Find(jointId);
            Cuisine cuisine = Cuisine.Find(cuisineId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("joint", joint);
            model.Add("cuisine", cuisine);
            Joint.Delete(jointId);
            return View(model);
        }



    }
}